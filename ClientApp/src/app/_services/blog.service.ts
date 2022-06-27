import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Blog } from '../_models/blog-model';
import { TypeBlog } from '../_models/type-blog-model';

@Injectable({ providedIn: 'root' })
export class BlogService extends GenericHttpService<Blog> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('Blog/filter', filter);
    }

    getAllByFilter(filter: any) {
      return this.postAll('Blog/getAllByFilter', filter);
    }

    

    save(entity:FormData) {
      return this.post('Blog/save', entity);
   }

    deleteById(id) {
          return this.delete(`Blog/${id}`);
    }

    getById(id: any) {
      return this.http.get<Blog>(`${this.getUrlApi()}Blog/${id}`);
  }

}
