import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertService } from '../core/service/alert.service';
import { Filme } from './filme.model';
import { FilmeService } from './filme.service';
import { SearchSettingsModel } from '@syncfusion/ej2-grids';
import { DataManager } from '@syncfusion/ej2-data';

@Component({
    selector: 'app-filme',
    templateUrl: './filme.component.html',
    providers: []
  })
  export class FilmeComponent {
    @ViewChild('myPersistenceModal') myModal;
    public entity: Filme = new Filme();
    public edicao: boolean = false;

    registerForm: FormGroup;
    submitted = false;
    public NomeFilter: string = ' ';

    public data: DataManager;
    public pageSettings: Object;  
    public searchSettings: SearchSettingsModel;
    
    constructor(public _route: ActivatedRoute, 
      public _router: Router,
      public _service: FilmeService, 
      public _alert: AlertService, 
      private formBuilder: FormBuilder,
      private spinnerService: NgxSpinnerService) {
     }
  
    ngOnInit() {
      this.pageSettings = { pageSizes: true, pageSize: 10 };  
      this.edicao = false;
      
      this.registerForm = this.formBuilder.group({
        Nome: ['', Validators.required],
        Catalogo: ['', Validators.required],
        Ano: ['', Validators.required],
        Slogan: ['', Validators.required],
        VisaoGeral: ['', Validators.required],
        Imagem: ['', Validators.required],
        Classificacao: ['', Validators.required]
      });

      this.load();
    }
  
    //#region Listagem

    load(){

      this.spinnerService.show();

      this._service.getFilter(this.NomeFilter).then( result =>{
        this.data = result;
        this.spinnerService.hide();
      })
    }

    back(){
      this._router.navigate(['/dashboard']);
    }

    new(){
      this.entity = new Filme();
      this.edicao = false;
      this.myModal.show();
    }
  
    //#endregion
    
    get f() { return this.registerForm.controls; }
    
    //#region Cadastro
    openModal(codigo: number){
      this._service.get(codigo).then(result =>{
          this.entity = result;
          this.edicao = true;
          this.myModal.show();
      })
    }

    onSubmit() {
      this.spinnerService.show();
      this.submitted = true;
  
      if (this.registerForm.invalid) {
        this.spinnerService.hide();
        return;
      }
  
      //se for inserção, coloca Guid empty nos códigos
      if (!this.edicao) {
        this.entity.Codigo = 0;
      }
  
      //salva
      this._service.save(this.entity).then(result => {
        if (result.success){
          this.spinnerService.hide();
          this.myModal.hide();
          this._alert.success("Registro salvo com sucesso.");
          this.load();
        }
        else{
          this._alert.error(result.errorMessage);
        }
      });
    }
    //#endregion
  }
  