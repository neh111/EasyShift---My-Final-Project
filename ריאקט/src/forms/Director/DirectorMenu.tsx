import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import Container from '@mui/material/Container';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import { Navigate } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import { color } from '@mui/system';
import { red, yellow } from '@mui/material/colors';
import EmbedShift from './embedShift';
import GeneralSchedule from './generalSchedule';
import axios from "axios";

const pages = ['יציאה', 'צפיה בתוצאות השיבוץ של כלל העובדים', 'שבץ שבוע זה', 'שינוי משמרות', 'הכנס בקשות לשיבוץ'];

const DirectorMenu = () => {
  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };
  const url = "https://localhost:44336/api/Employee/";
  let shifrArrTimes = ["jnj"];
  let shifrArrDays = ["s"];
  React.useEffect(
    () => {
      axios.get(url + 'GoToDaysNames').then(res => { localStorage.setItem("ArrDays",  JSON.stringify(res.data)); shifrArrDays = res.data }).then(i => {
        axios.get(url + 'GoToGetAllShiftsType').then(res => { localStorage.setItem("ArrTimes",  JSON.stringify(res.data)); shifrArrTimes = res.data }).
          then(x => {
            localStorage.setItem("DaysLenght", JSON.stringify(shifrArrDays.length));
            localStorage.setItem("TimesLenght", JSON.stringify(shifrArrTimes.length));
            // localStorage.setItem("ArrDays", JSON.stringify(shifrArrDays));
            // localStorage.setItem("ArrTimes", JSON.stringify(shifrArrTimes));
          })
      })
    },
    [],
  );
  let navigate = useNavigate()
  const onClick = (page: String) => {
    switch (page) {
      case 'שבץ שבוע זה':
        navigate('/DelayingAppearance');
        break;
      case 'צפיה בתוצאות השיבוץ של כלל העובדים':
        navigate('/InputsDate');
        break;
      case 'שינוי משמרות':
        navigate('/changeTableFormat');
        break;
      case 'הכנס בקשות לשיבוץ':
        navigate('/Requesttable');
        break;
      case 'יציאה':
        localStorage.clear();
        navigate('/');
        break;
    }
  }
  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' }, direction: "rtl" }}>
            {pages.map((page) => (
              <Button
                key={page}
                onClick={() => onClick(page)}
                sx={{ my: 2, color: 'white', display: 'block' }}>
                {page}
              </Button>
            ))}
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};
export default DirectorMenu;
