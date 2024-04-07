import { Member } from "./member";

export interface CommentInterface {
    id: string,
    commentContent: string,
    createdAt: string,
    isEdited: boolean,
    likes: number,
    isCommentLikedByCurrentUser: boolean,
    user: Member
}