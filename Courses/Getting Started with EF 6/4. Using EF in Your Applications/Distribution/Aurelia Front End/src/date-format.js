import moment from 'moment';

export class dateFormatValueConverter {
  toView(value) {
    return moment(value).format('M/D/YYYY');
  }
}