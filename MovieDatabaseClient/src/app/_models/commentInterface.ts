import { Member } from "./member";

export interface CommentInterface {
    id: string,
    commentContent: string,
    createdAt: string,
    likes: number,
    isEdited: boolean,
    user: Member
}