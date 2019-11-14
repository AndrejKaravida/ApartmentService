import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'approved'
})
export class ApprovedPipe implements PipeTransform {

  transform(value: any, ...args: any[]): any {

    if (value) { 
      return 'Approved';
    }

      else { 
      return 'Not Approved';
    }
  }

}
