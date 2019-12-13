import { LevelsServiceService } from './../services/levels-service.service';
import { AsyncValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


export function ValidatorsExistsName(leveServices: LevelsServiceService): AsyncValidatorFn {
    return (control: AbstractControl):
        Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
        return leveServices.checkExistsName(control.value).pipe(
            map(res => {
                // tslint:disable-next-line: object-literal-key-quotes
                return (res === true) ? { 'checkLevelName': true } : null;
            })
        );
    };
}



