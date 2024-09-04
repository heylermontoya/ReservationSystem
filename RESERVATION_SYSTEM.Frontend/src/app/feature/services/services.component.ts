import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ConfirmationService } from 'primeng/api';
import { FieldFilter } from '../../shared/interfaces/FieldFilter.interface';
import { ServicesService } from '../../shared/services/Service/services.service';
import { Service } from '../../shared/interfaces/Service.interface';
import { FormServiceComponent } from './component/form-service/form-service.component';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrl: './services.component.scss'
})
export class ServicesComponent {
  services!: Service[];
  searchValue = '';
  loading: boolean = true;
  ref!: DynamicDialogRef;
  filters: FieldFilter[] = [];

  constructor(
    private serviceService: ServicesService, 
    private router : Router,
    public dialogService: DialogService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.getService();
  } 

  getService(searchTerm: string = ''){
    this.serviceService.getService(this.filters).subscribe({
      next: response => {
        this.services = response;
        this.loading = false;

        this.services.forEach((service) => {
          service.available = service.available ? 'Yes' : 'No';
        });
      }
    })
  }

  newService(){
    this.ref = this.dialogService.open(FormServiceComponent, {
      data: { action:'create' },
      width: '90%',
      height: '100%',
      contentStyle: { "max-height": "700px", "overflow": "auto" }
    });

    this.ref.onClose.subscribe(() => {      
        this.getService();      
    });
    
  }

  editService(id:string,name:string,description:string,price:string,capacity:string){
    this.ref = this.dialogService.open(FormServiceComponent, {
      data: { 
        action:'update',
        id: id,
        name: name,
        description:description,
        price:price,
        capacity:capacity
      },
      width: '90%',
      height: '100%',
      contentStyle: { "max-height": "700px", "overflow": "auto" }
    });

    this.ref.onClose.subscribe(() => {      
      this.getService();      
    });
  }

  deleteService(id: string){
    this.confirmationService.confirm({
      message: '¿Estás seguro de que deseas eliminar este registro?',
      header: 'Confirmar Eliminación',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.serviceService.deleteService(id).subscribe({
          next: () => {
            this.getService();
          }
        })
      },
      reject: () => {
        console.log('Eliminación cancelada');
      }
    });
  }

  search() {
    if (this.searchValue.trim() === '') {
      this.getService();
    } else {
      this.getService(this.searchValue);
    }
  }

  onColumnFilter(event: any, field: string) {
    const value = event.target.value.trim();

    const data = {
      field: field,
      value : value
    }


    const indiceExist = this.filters.findIndex(item => item.field === data.field);

    if(indiceExist !== -1) {
      this.filters.splice(indiceExist, 1);
    }

    if(data.value){
      this.filters.push(data);
    }

    this.getService();
  }
}
