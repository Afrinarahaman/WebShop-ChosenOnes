import { Injectable } from '@angular/core';
import { Category } from 'src/app/_models/category';
import { environment } from '../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CategoryService {
    private apiUrl = environment.apiUrl + '/category';

    private httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    constructor(private http: HttpClient) { }

    getAllCategories(): Observable<Category[]> {
        return this.http.get<Category[]>(this.apiUrl);
    }

    getCategory(categoryId: number): Observable<Category> {
        return this.http.get<Category>(`${this.apiUrl}/${categoryId}`);
    }

    getCategoriesWithoutProducts(): Observable<Category[]>{
       return this.http.get<Category[]>(`${this.apiUrl}/WithoutProducts`)
    }

    addCategory(category: Category): Observable<Category> {
        return this.http.post<Category>(this.apiUrl, category, this.httpOptions);
      }


    editCategory(categoryId: number, category: Category): Observable<Category> {
        return this.http.put<Category>(`${this.apiUrl}/${categoryId}`, category, this.httpOptions);
    }

    deleteCategory(authorId: number): Observable<Category> {
        return this.http.delete<Category>(`${this.apiUrl}/${authorId}`, this.httpOptions);

    }
}



