import { userImage } from "./userImage";

export interface Member {
    id: string,
    userName: string,
    gender: string,
    introduction: string,
    created: string,
    localization: string,
    imageUrl: string,
    userImages: userImage[]
}