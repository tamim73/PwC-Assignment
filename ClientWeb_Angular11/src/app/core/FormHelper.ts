import { AbstractControl, FormGroup } from '@angular/forms';

export interface AssignmentFormField {
  name: string;
  label: string;
  type: string;
  errors: { [key: string]: string };
  placeholder?: string;
  options?: { label: string; value: any }[];
  hint?: string;
}

export const isEmptyInputValue = (value: any) =>
  value == null || value.length === 0;

export function matchValidator(targetCtrlName: string) {
  return (control: AbstractControl) => {
    if (isEmptyInputValue(control.value) || isEmptyInputValue(targetCtrlName)) {
      return null;
    }
    const target = control.parent?.get(targetCtrlName);
    if (target && control.value !== target.value) {
      return { match: true };
    }
    return null;
  };
}
