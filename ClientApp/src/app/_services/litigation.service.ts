import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })

export class LitigationService {

    constructor() {

    }

getWeight() {
    return [
        { label: '36kg', id: 36, values: {iniciante: 17.49, intermediario: 16.37, avancado: 15.90}},
        { label: '38kg', id: 38, values: {iniciante: 18.42, intermediario: 17.25, avancado: 16.75}},
        { label: '40kg', id: 40, values: {iniciante: 18.70, intermediario: 17.50, avancado: 17.00}},
        { label: '43kg', id: 43, values: {iniciante: 19.47, intermediario: 18.23, avancado: 17.70}},
        { label: '45kg', id: 45, values: {iniciante: 19.80, intermediario: 18.54, avancado: 18.00}},
        { label: '49kg', id: 49, values: {iniciante: 21.15, intermediario: 19.77, avancado: 19.20}},
        { label: '55kg', id: 55, values: {iniciante: 23.00, intermediario: 21.52, avancado: 20.90}},
        { label: '59kg', id: 59, values: {iniciante: 23.34, intermediario: 21.85, avancado: 21.22}},
        { label: '63kg', id: 63, values: {iniciante: 24.25, intermediario: 22.71, avancado: 22.05}},
        { label: '66kg', id: 66, values: {iniciante: 25.41, intermediario: 23.79, avancado: 23.10}},
        { label: '68kg', id: 68, values: {iniciante: 26.19, intermediario: 24.52, avancado: 23.81}},
        { label: '70kg', id: 70, values: {iniciante: 26.95, intermediario: 25.23, avancado: 24.50}},
        { label: '72kg', id: 72, values: {iniciante: 27.72, intermediario: 25.95, avancado: 25.20}},
        { label: '74kg', id: 74, values: {iniciante: 28.49, intermediario: 26.77, avancado: 25.90}},
        { label: '77kg', id: 77, values: {iniciante: 29.64, intermediario: 27.75, avancado: 26.95}},
        { label: '79kg', id: 79, values: {iniciante: 30.41, intermediario: 28.48, avancado: 27.65}},
        { label: '81kg', id: 81, values: {iniciante: 31.18, intermediario: 29.20, avancado: 28.35}},
        { label: '83kg', id: 83, values: {iniciante: 31.95, intermediario: 29.92, avancado: 29.05}},
        { label: '86kg', id: 86, values: {iniciante: 33.11, intermediario: 31.00, avancado: 30.10}},
        { label: '88kg', id: 88, values: {iniciante: 33.88, intermediario: 31.72, avancado: 30.80}},
        { label: '90kg', id: 90, values: {iniciante: 34.65, intermediario: 32.44, avancado: 31.50}},
        { label: '95kg', id: 95, values: {iniciante: 36.57, intermediario: 34.24, avancado: 33.25}},
        { label: '99kg', id: 99, values: {iniciante: 39.25, intermediario: 36.71, avancado: 35.65}},
        { label: '104kg', id: 104, values: {iniciante: 42.26, intermediario: 39.63, avancado: 38.48}},
        { label: '108kg', id: 108, values: {iniciante: 45.16, intermediario: 42.27, avancado: 41.04}}  
      ];
}

getLevels() {
    return [
        {label: 'Iniciante', value: 'iniciante'},
        {label: 'Intermediário', value: 'intermediario'},
        {label: 'Avançado', value: 'avancado'},
    ]
}

}
