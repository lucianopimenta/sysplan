import { Injectable } from '@angular/core';
import { ToasterService, Toast } from 'angular2-toaster';

@Injectable()
export class AlertService {

    constructor(private toastService: ToasterService) { }

    default(message: string, duration: number = 0) {
        this.alert('Mensagem', 'default', message, duration);
    }

    success(message: string, duration: number = 0) {
        this.alert('Sucesso', 'success', message, duration);
    }

    error(message: string, duration: number = 0) {
        this.alert('Erro', 'error', message, duration);
    }

    info(message: string, duration: number = 0) {
        this.alert('Informação', 'info', message, duration);
    }

    warn(message: string, duration: number = 0) {
        this.alert('Atenção', 'warning', message, duration);
    }

    private alert(title: string, type: string, message: string, duration: number) {
        var toast: Toast = {
            type: type,
            title: title,
            body: message,
            showCloseButton: true
        };
        
        this.toastService.pop(toast);
    }
}