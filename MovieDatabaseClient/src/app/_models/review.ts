import { Member } from "./member";
import { Movie } from "./movie";

export interface Review {
    id: string,
    rating: number,
    reviewContent: string,
    dateAdded: string,
    movie: Movie,
    user: Member
}