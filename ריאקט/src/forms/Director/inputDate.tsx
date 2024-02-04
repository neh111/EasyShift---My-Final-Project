import * as React from 'react';
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
// import SeeTableAccordingPosition from '../set-priority-to-shifts/TableAccordingPosition';
import { Button } from '@mui/material';
import CollapsibleTable from './generalSchedule';
import GeneralSchedule from './generalSchedule';
export default function InputsDate() {
  const [valueStart, setValuStart] = React.useState<Date | null>(
    new Date(),
  );
  const [valueEnd, setValuEnd] = React.useState<Date | null>(
    new Date(),
  );
  const handleChangeS = (newValue: Date | null) => {
    setValuStart(newValue);
  };
  const handleChangeE = (newValue: Date | null) => {
    setValuEnd(newValue);
  };
const [show,setShow]=React.useState(false)
  return (
    <>
      <LocalizationProvider dateAdapter={AdapterDateFns}>
        <Stack spacing={3}>
          <DesktopDatePicker
            label="Date start"
            inputFormat="MM/dd/yyyy"
            value={valueStart}
            onChange={handleChangeS}
            renderInput={(params) => <TextField {...params} />}
          />
          <DesktopDatePicker
            label="Date end"
            inputFormat="MM/dd/yyyy"
            value={valueEnd}
            onChange={handleChangeE}
            renderInput={(params) => <TextField {...params} />}
          />
        </Stack>
      </LocalizationProvider>
      <Button onClick={()=>setShow(true)}>שלח</Button>
     {show&&<GeneralSchedule start={valueStart} end={valueEnd}/>} 
      
    </>
  );
}

// "@date-io/core": {
//   "version": "2.14.0",
//   "resolved": "https://registry.npmjs.org/@date-io/core/-/core-2.14.0.tgz",
//   "integrity": "sha512-qFN64hiFjmlDHJhu+9xMkdfDG2jLsggNxKXglnekUpXSq8faiqZgtHm2lsHCUuaPDTV6wuXHcCl8J1GQ5wLmPw=="
// },
// "@date-io/date-fns": {
//   "version": "2.14.0",
//   "resolved": "https://registry.npmjs.org/@date-io/date-fns/-/date-fns-2.14.0.tgz",
//   "integrity": "sha512-4fJctdVyOd5cKIKGaWUM+s3MUXMuzkZaHuTY15PH70kU1YTMrCoauA7hgQVx9qj0ZEbGrH9VSPYJYnYro7nKiA==",
//   "requires": {
//     "@date-io/core": "^2.14.0"
//   }
// },
// "@date-io/dayjs": {
//   "version": "2.14.0",
//   "resolved": "https://registry.npmjs.org/@date-io/dayjs/-/dayjs-2.14.0.tgz",
//   "integrity": "sha512-4fRvNWaOh7AjvOyJ4h6FYMS7VHLQnIEeAV5ahv6sKYWx+1g1UwYup8h7+gPuoF+sW2hTScxi7PVaba2Jk/U8Og==",
//   "requires": {
//     "@date-io/core": "^2.14.0"
//   }
// },
// "@date-io/luxon": {
//   "version": "2.14.0",
//   "resolved": "https://registry.npmjs.org/@date-io/luxon/-/luxon-2.14.0.tgz",
//   "integrity": "sha512-KmpBKkQFJ/YwZgVd0T3h+br/O0uL9ZdE7mn903VPAG2ZZncEmaUfUdYKFT7v7GyIKJ4KzCp379CRthEbxevEVg==",
//   "requires": {
//     "@date-io/core": "^2.14.0"
//   }
// },
// "@date-io/moment": {
//   "version": "2.14.0",
//   "resolved": "https://registry.npmjs.org/@date-io/moment/-/moment-2.14.0.tgz",
//   "integrity": "sha512-VsoLXs94GsZ49ecWuvFbsa081zEv2xxG7d+izJsqGa2L8RPZLlwk27ANh87+SNnOUpp+qy2AoCAf0mx4XXhioA==",
//   "requires": {
//     "@date-io/core": "^2.14.0"
//   }
// },