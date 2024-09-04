import { Component } from '@angular/core';
import { User } from '../../shared/interfaces/user.interface';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CustomerService } from '../../shared/services/customer/customer.service';
import { FormUserComponent } from './component/form-user/form-user.component';
import { ConfirmationService } from 'primeng/api';
import { FieldFilter } from '../../shared/interfaces/FieldFilter.interface';

@Component({
  selector: 'app-users',  
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {

  users!: User[];
  searchValue = '';
  loading: boolean = true;
  ref!: DynamicDialogRef;
  filters: FieldFilter[] = [];

  constructor(
    private customerService: CustomerService, 
    public dialogService: DialogService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.getUser();
  } 

  getUser(){
    this.customerService.getUser(this.filters).subscribe({
      next: response => {
        this.users = response;
        this.loading = false;

        this.users.forEach((user) => {
          user.dateRegistration = new Date(<Date>user.dateRegistration);
        });
      }
    })
  }

  newUser(){
    this.ref = this.dialogService.open(FormUserComponent, {
      data: { action:'create' },
      width: '90%',
      height: '100%',
      contentStyle: { "max-height": "700px", "overflow": "auto" }
    });

    this.ref.onClose.subscribe(() => {      
        this.getUser();      
    });    
  }

  editUser(id:string,name:string,phone:string,email:string){
    this.ref = this.dialogService.open(FormUserComponent, {
      data: { 
        action:'update',
        id: id,
        name: name,
        phone:phone,
        email:email
      },
      width: '90%',
      height: '100%',
      contentStyle: { "max-height": "700px", "overflow": "auto" }
    });

    this.ref.onClose.subscribe(() => {      
      this.getUser();      
    });
  }

  deleteUser(id: string){
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this record?',
      header: 'Confirm Deletion',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.customerService.deleteUser(id).subscribe({
          next: () => {
            this.getUser();
          }
        })
      },
      reject: () => {
        console.log('Deletion canceled');
      }
    });
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

    this.getUser(); 
  }

  onDateFilter(event: any, field: string) {
    const value = event ? new Date(event).toISOString().split('T')[0] : '';

    const data = {
        field: field,
        value: value,
        typeDateTime: 3
    };

    const indiceExist = this.filters.findIndex(item => item.field === data.field);

    if (indiceExist !== -1) {
        this.filters.splice(indiceExist, 1);
    }

    if (data.value) {
        this.filters.push(data);
    }

    this.getUser(); 
  }
}
