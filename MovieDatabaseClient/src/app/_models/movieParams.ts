export class MovieParams {
    pageNumber = 1;
    pageSize = 8;
    genre = '';
    releaseDate: string | null = null;
    orderBy = 'popularity';
}