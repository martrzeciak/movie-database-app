import { Member } from "./member";

export interface CommentInterface {
    id: string,
    commentContent: string,
    createdAt: string,
    isEdited: boolean,
    user: Member
}