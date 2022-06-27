import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class LevelService {
private levels: any[] = [];
    constructor() {
        this.loadLevel();
    }

    loadLevel() {
        this.levels.push({value: 4, name: 'Iniciante'});
        this.levels.push({value: 5, name: 'Intermediário'});
        this.levels.push({value: 6, name: 'Avançado'});
      }

      getNameLevel(value) {
        return this.levels.find(x => x.value === value).name;
      }

      get() {
          return this.levels;
      }

}
