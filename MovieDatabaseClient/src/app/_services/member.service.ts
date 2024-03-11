import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { MemberParams } from '../_models/memberParams';
import { HttpClient } from '@angular/common/http';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  memberCache = new Map();
  memberParams: MemberParams | undefined;

  constructor(private http: HttpClient) { }

  getUserParams() {
    return this.memberParams;
  }

  setUserParams(params: MemberParams) {
    this.memberParams = params;
  }

  resetUserParams() {
      this.memberParams = new MemberParams();
      return this.memberParams;
  }

  getMembers(memberParams: MemberParams) {
    const response = this.memberCache.get(Object.values(memberParams).join('-'));

    if (response) return of(response);

    let params = getPaginationHeaders(memberParams.pageNumber, memberParams.pageSize);

    return getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http).pipe(
      map(response => {
        this.memberCache.set(Object.values(memberParams).join('-'), response);
        return response;
      })
    );
  }

  getMember(username: string) {
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: Member) => member.userName === username);

    if (member) return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map( () => {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member};
        console.log('dsad')
      })
    )   
  }
}
