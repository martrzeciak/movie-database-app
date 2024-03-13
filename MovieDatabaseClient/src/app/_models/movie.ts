import { Genre } from "./genre";

export interface Movie {
    id: string,
    title: string,
    releaseDate: number,
    durationInMinutes: number,
    director: string,
    description: string,
    posterUrl: string,
    averageRating: number,
    ratingCount: number,
    genres: Genre[]
}
