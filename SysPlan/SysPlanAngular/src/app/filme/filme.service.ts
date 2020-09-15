import 'rxjs/add/Observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/toPromise';

import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Filme } from './filme.model';
import { AlertService } from '../core/service/alert.service';
import { Result } from '../core/model/result';

@Injectable()
export class FilmeService {

    constructor(
        public http: HttpClient, 
        public alert: AlertService) { 
    }

    async get(codigo: number): Promise<Filme>{ 
        
        let response = await this.http.get<Result>("filme/" + codigo).toPromise();
            
        if (response.success) {
            return response.data as Promise<Filme>;
        }
        else {
            this.alert.error(response.errorMessage);
        }
    }
    
    createNew(): Filme{
        return new Filme();
    }   

    async getAll(): Promise<any>{ 
        try {
            let response = await this.http.get<Result>("filme").toPromise();
            
            if (response.success) {
                return response.data as Promise<Filme>;
            }
            else {
                this.alert.error(response.errorMessage);
            }
        } catch (error) {
            this.alert.error(error.message);
        }
        
        return null;
    }

    async getFilter(nome: string): Promise<any>{ 
        try {
            let response = await this.http.get<Result>("filme?nome=" + nome).toPromise();
            
            if (response.success) {
                return response.data as Promise<Filme>;
            }
            else {
                this.alert.error(response.errorMessage);
            }
        } catch (error) {
            this.alert.error(error.message);
        }
        
        return null;
    }

    async remove(codigo: number) {
        let response = await this.http.delete<Result>("filme/" + codigo).toPromise();
        return response.success;
    }

    async save(entidade: Filme): Promise<Result>{

        if(entidade.Codigo == 0){
            return await this.http.post("filme", entidade).toPromise() as Result;
        } else {
            return await this.http.put("filme", entidade).toPromise() as Result;
        }
    }
}