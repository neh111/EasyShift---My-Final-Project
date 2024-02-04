import React from 'react';
import logo from './logo.svg';
import './App.css';
import css from './Styles.module.css';
import Logo from './logo/logo';
import ChoosePriorityPerShiftF from './forms/set-priority-to-shifts/choosePriorityPerShift';
import SetPriorityTable from './forms/set-priority-to-shifts/choosePriorityPerShift';
import { BrowserRouter, Link, Routes, Route } from "react-router-dom";
import EmployeeMenu from './forms/Employee/emplyeeMenu';
// import { Link } from '@material-ui/core';
import HomePage from './forms/home -page';
import SignUp from './forms/Employee/signUp';
import SignIn from './forms/Employee/signIn';
// import ComboBox from './job';
// import { DirectorMy } from './forms/Director/signUpDirector';
import ContactUs from './forms/ContactUs/ContactUs';
// import SeeTableAccordingPosition from './forms/set-priority-to-shifts/TableAccordingPosition';
import InputsData from './forms/Employee/inputDate';
import DirectorMy2 from './forms/Director/signUpDirector';
import GeneralSchedule from './forms/Director/generalSchedule';
import EmbedShift from './forms/Director/embedShift';
import DelayingAppearance from './forms/Director/DelayingAppearance';
import DirectorMenu from './forms/Director/DirectorMenu';
// import A from './forms/set-priority-to-shifts/TableAccordingJob'
import Checkboxes from './forms/Director/changeTry';
import RequestTable from './forms/Director/RequestShift';
import CheckTry from './forms/Director/CheckTry';
import ChangeShift from './forms/Director/changeShift';
import PlacmentResultPerEmp from './forms/Employee/inputDatePlacmentResultPerEmp';
import InputsDate from './forms/Director/inputDate';
// import Request from './forms/Director/RequestShift';
// import RequestTable2 from './forms/Director/RequestShift';
import ColumnTypesGrid from './forms/set-priority-to-shifts/choosePriorityPerShift';
function App() {
  return (
    <div className="App">
      <Logo />
      {/* <DelayingAppearance/> */}
      {/* <BasicTextFields /> */}
      {/* <SignUp/> */}
      {/* <DirectorMy/> */}
      {/* <SignIn/> */}
      {/* <ChoosePriorityPerShiftF />
      {/* <HomePage/> */}
      {/* <ContactUs/> */}
      {/* <EmployeeMenu/> */}
      {/* <InputsData/> */}
      {/* <SeeTableAccordingPosition /> */}
      {/* start={01/01/2020} end={02/02/2022} */}
      {/* <A/> */}
      {/* <RequestTable/> */}
      {/* <Checkboxes/> */}
      {/* <ChangeShift numb={5}/> */}
      {/* <CheckTry/> */}
      {/* <Request/> */}
      {/* <GeneralSchedule/> */}
      {/* <RequestTable/> */}
      {/* <RequestTable2/> */}
      {/* <ColumnTypesGrid/> */}
      <Routes>
        {/* <Route path='sign-up-my' element={<SignUp />} /> */}
        <Route path='sign-in-my' element={<SignIn />} />
        <Route path='sign-up-my' element={<SignUp />} />
        <Route path='TableShift' element={<SetPriorityTable />} />
        <Route path='EmployeeMenu' element={<EmployeeMenu />} />
        <Route path='/' element={<HomePage />} />
        <Route path='Director' element={<DirectorMy2 />} />
        <Route path='ChoosePriorityPerShift' element={<ChoosePriorityPerShiftF />} />
        {/* <Route path='Schedule' element={<ChoosePriorityPerShiftF />} /> */}
        {/* <Route path='GeneralSchedule' element={<GeneralSchedule />} /> */}
        <Route path='EmbedShift' element={<EmbedShift/>} />
        <Route path='DirectorMenu' element={<DirectorMenu/>} />
        <Route path='InputDate' element={<InputsData/>} />
        <Route path='Schedule' element={<PlacmentResultPerEmp/>} />
        <Route path='Requesttable' element={<RequestTable/>} />
        <Route path='changeTableFormat' element={<CheckTry/>} />
        {/* <Route path='SeeTableAccordingPosition' element={<SeeTableAccordingPosition />} /> */}
        <Route path='Checkboxes' element={<Checkboxes/>}/>
        <Route path='InputsDate' element={<InputsDate/>}/>
        <Route path='DelayingAppearance' element={<DelayingAppearance/>}/>
        
      </Routes>
    </div>
  )
}
// function App() {
//   return (
//   );
// }

export default App;
