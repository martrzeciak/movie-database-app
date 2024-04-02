import { ActorImage } from "./actorImage"

export interface Actor {
    id: string,
    firstName: string,
    lastName: string,
    dateOfBirth: Date,
    birthplace: string,
    biography: string,
    heightInCentimeters: number,
    gender: string,
    averageRating: number,
    ratingCount: number,
    imageUrl: string
    actorPosition: number,
    images: ActorImage[]
}

