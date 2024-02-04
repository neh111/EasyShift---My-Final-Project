import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { Grid } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { relative } from 'path';
import { color } from '@mui/system';

export default function HomePage() {
    let navigate = useNavigate();
  const GoToSighnIn = () => {
    navigate('/sign-in-my');
  }
  const GoToSighnUp = () => {
    navigate('/sign-up-my');
  }
  const GoToSighnUpDirector = () => {
    navigate('/Director');
  }
  return (
    <Card sx={{ maxWidth: 420,position:'relative', 'left':530}}>
      <CardMedia
        component="img"
        height="370"
        image="./images/home-page.png"
      />
      <CardActions>
      <Grid item xs>
    <Button variant="outlined" style={{background:"blue",color:"yellow"}} type="submit" onClick={GoToSighnUp} >עובד חדש </Button>
  </Grid>
  <Grid item xs>
    <Button variant="outlined"style={{background:"blue",color:"yellow"}} type="submit" onClick={GoToSighnIn} >עובד קיים </Button>
  </Grid>
  <Grid item xs>
    <Button variant="outlined"style={{background:"blue",color:"yellow"}} type="submit" onClick={GoToSighnUpDirector} >היכנס כמנהל  </Button>
  </Grid>
      </CardActions>
    </Card>
  );
}
