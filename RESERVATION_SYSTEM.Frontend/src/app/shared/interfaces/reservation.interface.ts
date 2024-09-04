export interface Reservation{
    id:string;
    customerId: string;
    customerName: string;
    ServiceId: string;
    serviceName: string;
    dateReservation: string | Date;
    startDate: string | Date;
    endDate: string | Date;
    state: string;
    numberPeople: string;
    total: string;
}
