namespace ClinicDataAccessLayer.Entities.Enums;

public enum AppointmentStatus
{
    Pending = 0,      // The appointment has been scheduled but has not yet occurred
    Confirmed = 1,    // The appointment has been confirmed by both the patient and the healthcare provider
    Completed = 2,    // The appointment has taken place as scheduled
    Canceled = 3,     // The appointment has been canceled either by the patient or the healthcare provider
    Rescheduled = 4,  // The appointment has been rescheduled for a different date or time
    NoShow = 5        // The patient did not show up for the appointment without canceling or rescheduling
}