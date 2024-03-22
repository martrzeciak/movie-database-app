export class MovieParams {
    pageNumber = 1;
    pageSize = 12;
    genre = '';
    releaseDate: string | null = null;
    orderBy = 'popularity';
}