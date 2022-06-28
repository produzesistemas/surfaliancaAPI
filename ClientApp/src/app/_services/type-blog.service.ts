import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { TypeBlog } from '../_models/type-blog-model';

@Injectable({ providedIn: 'root' })
export class TypeBlogService extends GenericHttpService<TypeBlog> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getAll() {
        return this.http.get<TypeBlog[]>(`${this.getUrlApi()}Blog/getAllTypeBlog`);
    }
    
}
