import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { RegularExpressions } from '../../../../shared/constant/regex';
import { CreateOrUpdateReservation } from '../../../../shared/interfaces/createOrUpdateReservation.interface';
import { Service } from '../../../../shared/interfaces/Service.interface';
import { User } from '../../../../shared/interfaces/user.interface';
import { CustomerService } from '../../../../shared/services/customer/customer.service';
import { ReservationService } from '../../../../shared/services/reservation/reservation.service';
import { ServicesService } from '../../../../shared/services/Service/services.service';

export enum ReservationStatus {
  Confirmed = 'Confirmed',
  Canceled = 'Canceled',
  Modified = 'Modified'
}

@Component({
  selector: 'app-form-reservation',  
  templateUrl: './form-reservation.component.html',
  styleUrl: './form-reservation.component.scss'
})
export class FormReservationComponent {
  reservationForm: FormGroup;
  requiredField = 'Required field';
  reservationName = '';
  action = '';
  id= '';
  possitiveNumber = 'Positive number greater than or equal to zero';
  possitiveNumberInt = 'Positive number integer greater than or equal to zero';

  states = [
    { stateId: ReservationStatus.Confirmed, stateName: 'Confirmed' },
    { stateId: ReservationStatus.Canceled, stateName: 'Canceled' },
    { stateId: ReservationStatus.Modified, stateName: 'Modified' }
  ];

  services: Service[] = [];
  customers: User[] = [];

  constructor(    
    private formBuilder: FormBuilder, 
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private reservationService: ReservationService,
    private customerService: CustomerService,
    private serviceService: ServicesService
  ) {
    this.reservationForm = this.formBuilder.group({
      customer: [''],
      service: ['', Validators.required],
      dateReservation: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      numberPeople: [null,[Validators.required, Validators.min(0),Validators.pattern(RegularExpressions.NUMERIC)]],
      total: [null,[Validators.required, Validators.min(0),Validators.pattern(RegularExpressions.NUMERIC_PART)]]
    });
  }

  ngOnInit() {
    this.loadInformation();    

    this.action = this.config.data.action;

    this.reservationName = this.config.data.name;

    if (this.action === 'update'){
      this.id = this.config.data.id;
      this.reservationForm.patchValue({
        dateReservation: this.config.data.dateReservation,
        startDate: this.config.data.startDate,
        endDate: this.config.data.endDate,
        numberPeople: this.config.data.numberPeople,
        total: this.config.data.total
      });

      this.reservationForm.get('state')?.setValidators([Validators.required]);
      this.reservationForm.get('state')?.updateValueAndValidity();
    } else {
      this.reservationForm.get('customer')?.setValidators([Validators.required]);
      this.reservationForm.get('customer')?.updateValueAndValidity();
    }
  }

  loadInformation(){
    this.getCustomer();    
    this.getService();    
  }

  getCustomer(){
    this.customerService.getUser([]).subscribe({
      next: response => {
        this.customers = response;        
      }
    });
  }

  getService(){
    this.serviceService.getService([]).subscribe({
      next: response => {
        this.services = response;  
        if(this.action === 'update'){
          this.services = this.services.filter(
            service => service.available 
            || service.id === this.config.data.serviceId
          )   

          this.reservationForm.patchValue({
            service: this.config.data.serviceId            
          });

        } else {
          this.services = this.services.filter(
            service => service.available 
          )  
        }
           
      }
    });
  }
  onSubmit() {
    if (this.reservationForm.valid) {
      if(this.action === 'create') {
        const reservationParser = this.reservationCreateParserFormData();
        this.reservationService.createReservation(reservationParser).subscribe({
          next: () => {
                this.onClose();   
          }
        });
      } else{
        const reservationParser = this.reservationUpdateParserFormData();
        this.reservationService.updateReservation(reservationParser).subscribe({
          next: () => {
                this.onClose();   
          }
        });
      }

    }
  }

  onClose() {
    this.reservationForm.reset();
    this.ref.close();
  }

  private reservationCreateParserFormData(): CreateOrUpdateReservation{
    return {
      customerId: this.reservationForm.value.customer,
      ServiceId: this.reservationForm.value.service,
      dateReservation: this.reservationForm.value.dateReservation,
      startDate: this.reservationForm.value.startDate,
      endDate: this.reservationForm.value.endDate,
      numberPeople: this.reservationForm.value.numberPeople,
      total: this.reservationForm.value.total
    }
  }
  
  private reservationUpdateParserFormData(): CreateOrUpdateReservation{
    return {
      id: this.id,
      ServiceId: this.reservationForm.value.service,
      dateReservation: this.reservationForm.value.dateReservation,
      startDate: this.reservationForm.value.startDate,
      endDate: this.reservationForm.value.endDate,
      numberPeople: this.reservationForm.value.numberPeople,
      total: this.reservationForm.value.total
    }
  }
}
