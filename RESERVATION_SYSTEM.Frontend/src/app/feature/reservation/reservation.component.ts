import { Component } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ConfirmationService } from 'primeng/api';
import { FieldFilter } from '../../shared/interfaces/FieldFilter.interface';
import { Reservation } from '../../shared/interfaces/reservation.interface';
import { ReservationService } from '../../shared/services/reservation/reservation.service';
import { FormReservationComponent } from './component/form-reservation/form-reservation.component';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss'
})
export class ReservationComponent {

  reservations!: Reservation[];
  searchValue = '';
  loading: boolean = true;
  ref!: DynamicDialogRef;
  filters: FieldFilter[] = [];

  constructor(
    private reservationService: ReservationService, 
    public dialogService: DialogService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.getReservation();
  } 

  getReservation(searchTerm: string = ''){
    this.reservationService.getReservation(this.filters).subscribe({
      next: response => {
        this.reservations = response;
        this.loading = false;

        this.reservations.forEach((reservation) => {
          reservation.dateReservation = new Date(<Date>reservation.dateReservation);
          reservation.startDate = new Date(<Date>reservation.startDate);
          reservation.endDate = new Date(<Date>reservation.endDate);
        });
      }
    })
  }

  newReservation(){
    this.ref = this.dialogService.open(FormReservationComponent, {
      data: { action:'create' },
      width: '90%',
      height: '100%',
      contentStyle: { "max-height": "700px", "overflow": "auto" }
    });

    this.ref.onClose.subscribe(() => {      
        this.getReservation();      
    });
    
  }

  editReservation(
    id:string,
    customerId:string,
    serviceId:string,
    dateReservation:string,
    startDate:string,
    endDate:string,
    state:string,
    numberPeople:string,
    total:string
  ){
    this.ref = this.dialogService.open(FormReservationComponent, {
      data: { 
        action:'update',
        id: id,
        customerId:customerId,
        serviceId:serviceId,
        dateReservation:dateReservation,
        startDate:startDate,
        endDate:endDate,
        state:state,
        numberPeople:numberPeople,
        total:total
      },
      width: '90%',
      height: '100%',
      contentStyle: { "max-height": "700px", "overflow": "auto" }
    });

    this.ref.onClose.subscribe(() => {      
      this.getReservation();      
    });
  }

  deleteReservation(id: string){
    this.confirmationService.confirm({
      message: '¿Estás seguro de que deseas eliminar este registro?',
      header: 'Confirmar Eliminación',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.reservationService.deleteReservation(id).subscribe({
          next: () => {
            this.getReservation();
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
      this.getReservation();
    } else {
      this.getReservation(this.searchValue);
    }
  }

  onColumnFilter(event: any, field: string) {
    const value = event.target.value.trim();

    const data = {
      field: field,
      value : value
    }

    //
    const indiceExist = this.filters.findIndex(item => item.field === data.field);

    if(indiceExist !== -1) {
      this.filters.splice(indiceExist, 1);
    }

    if(data.value){
      this.filters.push(data);
    }

    //
    this.getReservation(); 
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

    this.getReservation(); 
  }
}
