import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent {

  constructor(private router: Router) { }

  navigateToAdminMovieList() {
    this.router.navigate(['movie-list']);
  }

  navigateToAdminActorList() {
    this.router.navigate(['actor-list']);
  }
}
