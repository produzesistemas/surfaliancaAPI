import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Team } from '../_models/team-model';

@Injectable({ providedIn: 'root' })
export class TeamService extends GenericHttpService<Team> {
private obj: Team = new Team();
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('team/filter', filter);
    }

    save(team: FormData) {
      return this.post('team/save', team);
   }

    deleteById(id) {
          return this.delete(`team/${id}`);
    }

    getById(id: any) {
      return this.http.get<Team>(`${this.getUrlApi()}team/${id}`);
  }
  getAll() {
    return this.http.get<Team[]>(`${this.getUrlApi()}team/getAll`);
}


}
