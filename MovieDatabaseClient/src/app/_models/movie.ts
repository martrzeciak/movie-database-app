import { Genre } from "./genre";
import { Poster } from "./poster";

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
    moviePosition: number,
    isOnWantToWatchMovie: boolean,
    genres: Genre[],
    posters: Poster[]
}
