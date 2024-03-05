import { Genre } from "./genre";

export interface Movie {
    id: string,
    title: string,
    releaseDate: number,
    durationInMinutes: number,
    director: string,
    description: string,
    posterUrl: string,
    genres: Genre[]
}