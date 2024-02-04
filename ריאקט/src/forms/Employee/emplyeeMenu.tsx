// import * as React from 'react';
// import AppBar from '@mui/material/AppBar';
// import Box from '@mui/material/Box';
// import Toolbar from '@mui/material/Toolbar';
// import IconButton from '@mui/material/IconButton';
// import Typography from '@mui/material/Typography';
// import Menu from '@mui/material/Menu';
// import Container from '@mui/material/Container';
// import Avatar from '@mui/material/Avatar';
// import Button from '@mui/material/Button';
// import Tooltip from '@mui/material/Tooltip';
// import MenuItem from '@mui/material/MenuItem';
// import { Navigate } from 'react-router-dom';
// import { useNavigate } from 'react-router-dom';
// import TableShift from '../set-priority-to-shifts/choosePriorityPerShift';
// import ShowPosition from '../../ShowPosition';
// import { color } from '@mui/system';
// import { red, yellow } from '@mui/material/colors';

// const pages = ['שיבוץ עצמי', 'צפיה בלוח משמרות לפי תפקיד', 'צפיה בתוצאות השיבוץ'];
// // const settings = ['Profile', 'Account', 'Dashboard', 'Logout'];

// const EmployeeMenu = () => {
//   const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
//   const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

//   const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
//     setAnchorElNav(event.currentTarget);
//   };
//   const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
//     setAnchorElUser(event.currentTarget);
//   };
//   // const handleCloseNavMenu = () => {
//   //   setAnchorElNav(null);
//   // };
//   // const handleCloseUserMenu = () => {
//   //   setAnchorElUser(null);
//   // };
//   let navigate = useNavigate()
//   const ChoosePriorityPerShiftF = (page: String) => {
//     switch (page) {
//       case 'שיבוץ עצמי':
//         navigate('/ChoosePriorityPerShift');
//         break;
//       case 'צפיה בלוח משמרות לפי תפקיד':
//         navigate('/InputDate');
//         break;
//       case 'צפיה בתוצאות השיבוץ':
//         navigate('/Schedule')
//     }
//   }
//   // const NumEmployeePerShift = () => {
//   //   navigate('/SeeTableAccordingPositin');
//   // }

//   return (
//     <AppBar position="static">
//       <Container maxWidth="xl">
//         <Toolbar disableGutters>
//           <Typography
//             variant="h6"
//             noWrap
//             component="div"
//             sx={{ mr: 2, display: { xs: 'none', md: 'flex' } }}
//           >
//           </Typography>
//           {/* <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}> */}
//           {/* <IconButton
//               size="large"
//               aria-label="account of current user"
//               aria-controls="menu-appbar"
//               aria-haspopup="true"
//               onClick={NumEmployeePerShift}
//               color="inherit"
//             >
//             </IconButton> */}
//           {/* <Menu
//               id="menu-appbar"
//               anchorEl={anchorElNav}
//               anchorOrigin={{
//                 vertical: 'bottom',
//                 horizontal: 'left',
//               }}
//               keepMounted
//               transformOrigin={{
//                 vertical: 'top',
//                 horizontal: 'left',
//               }}
//               open={Boolean(anchorElNav)}
//               onClose={handleCloseNavMenu}
//               sx={{
//                 display: { xs: 'block', md: 'none' },
//               }}
//             >
//               {pages.map((page) => (
//                 <MenuItem key={page} onClick={ChoosePriorityPerShift}>
//                   <Typography textAlign="center">{page}</Typography>
//                 </MenuItem>
//               ))}
//             </Menu> */}
//           {/* </Box>
//           <Typography
//             variant="h6"
//             noWrap
//             component="div"
//             sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}
//           >
//           </Typography>  */}
//           <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
//             {pages.map((page) => (
//               <Button
//                 key={page}
//                 onClick={() => ChoosePriorityPerShiftF(page)}
//                 sx={{ my: 2, color: 'white', display: 'block' }}
//               >
//                 {page}
//               </Button>
//             ))}
//           </Box>

//           {/* <Box sx={{ flexGrow: 0 }}>
//             <Tooltip title="Open settings">
//               <IconButton onClick={ChoosePriorityPerShift} sx={{ p: 0 }}>
//                 <Avatar alt="Remy Sharp" src="/static/images/avatar/2.jpg" />
//               </IconButton>
//             </Tooltip>
//             <Menu
//               sx={{ mt: '45px' }}
//               id="menu-appbar"
//               anchorEl={anchorElUser}
//               anchorOrigin={{
//                 vertical: 'top',
//                 horizontal: 'right',
//               }}
//               keepMounted
//               transformOrigin={{
//                 vertical: 'top',
//                 horizontal: 'right',
//               }}
//               open={Boolean(anchorElUser)}
//               onClose={ChoosePriorityPerShift}
//             >
//               {settings.map((setting) => (
//                 <MenuItem key={setting} onClick={NumEmployeePerShift}>
//                   <Typography textAlign="center" color={red}>{setting}</Typography>
//                 </MenuItem>
//               ))}
//             </Menu>
//           </Box> */}
//         </Toolbar>
//       </Container>
//     </AppBar>
//   );
// };
// export default EmployeeMenu;

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
import TableShift from '../set-priority-to-shifts/choosePriorityPerShift';
import ShowPosition from '../../ShowPosition';
import { color } from '@mui/system';
import { red, yellow } from '@mui/material/colors';
import axios from "axios";

const pages = ['שיבוץ עצמי', 'צפיה בלוח משמרות לפי תפקיד', 'צפיה בתוצאות השיבוץ', 'יציאה'];
// const settings = ['Profile', 'Account', 'Dashboard', 'Logout'];

const EmployeeMenu = () => {
  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };
  // const handleCloseNavMenu = () => {
  //   setAnchorElNav(null);
  // };
  // const handleCloseUserMenu = () => {
  //   setAnchorElUser(null);
  // };
  const url = "https://localhost:44336/api/Employee/";
  let shifrArrTimes = ["jnj"];
  let shifrArrDays = ["s"];
  React.useEffect(
    () => {
      axios.get(url + 'GoToDaysNames').then(res => { localStorage.setItem("ArrDays", JSON.stringify(res.data)); shifrArrDays = res.data }).then(i => {
        axios.get(url + 'GoToGetAllShiftsType').then(res => { localStorage.setItem("ArrTimes", JSON.stringify(res.data)); shifrArrTimes = res.data }).
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
  const ChoosePriorityPerShiftF = (page: String) => {
    switch (page) {
      case 'שיבוץ עצמי':
        navigate('/ChoosePriorityPerShift');
        break;
      case 'צפיה בלוח משמרות לפי תפקיד':
        navigate('/InputDate');
        break;
      case 'צפיה בתוצאות השיבוץ':
        navigate('/Schedule')
        break;
      case 'יציאה':
        localStorage.clear();
        navigate('/')
    }
  }
  // const NumEmployeePerShift = () => {
  //   navigate('/SeeTableAccordingPositin');
  // }

  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ mr: 2, display: { xs: 'none', md: 'flex' } }}
          >
          </Typography>
          {/* <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}> */}
          {/* <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={NumEmployeePerShift}
              color="inherit"
            >
            </IconButton> */}
          {/* <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'left',
              }}
              keepMounted
              transformOrigin={{
                vertical: 'top',
                horizontal: 'left',
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                display: { xs: 'block', md: 'none' },
              }}
            >
              {pages.map((page) => (
                <MenuItem key={page} onClick={ChoosePriorityPerShift}>
                  <Typography textAlign="center">{page}</Typography>
                </MenuItem>
              ))}
            </Menu> */}
          {/* </Box>
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}
          >
          </Typography>  */}
          <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex', direction: "rtl" } }}>
            {pages.map((page) => (
              <Button
                key={page}
                onClick={() => ChoosePriorityPerShiftF(page)}
                sx={{ my: 2, color: 'white', display: 'block' }}
              >
                {page}
              </Button>
            ))}
          </Box>

          {/* <Box sx={{ flexGrow: 0 }}>
            <Tooltip title="Open settings">
              <IconButton onClick={ChoosePriorityPerShift} sx={{ p: 0 }}>
                <Avatar alt="Remy Sharp" src="/static/images/avatar/2.jpg" />
              </IconButton>
            </Tooltip>
            <Menu
              sx={{ mt: '45px' }}
              id="menu-appbar"
              anchorEl={anchorElUser}
              anchorOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              keepMounted
              transformOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              open={Boolean(anchorElUser)}
              onClose={ChoosePriorityPerShift}
            >
              {settings.map((setting) => (
                <MenuItem key={setting} onClick={NumEmployeePerShift}>
                  <Typography textAlign="center" color={red}>{setting}</Typography>
                </MenuItem>
              ))}
            </Menu>
          </Box> */}
        </Toolbar>
      </Container>
    </AppBar>
  );
};
export default EmployeeMenu;