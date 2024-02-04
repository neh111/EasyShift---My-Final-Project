import { Button, ButtonGroup, Checkbox, createMuiTheme, makeStyles, TextField, ThemeProvider, FormControlLabel } from "@material-ui/core";
import { lightBlue, orange } from "@material-ui/core/colors";
import axios from "axios";
import { Component } from "react";
import Logo from "../../logo/logo";



import "./ContactUs.css";

function ContactUs(): JSX.Element {

    const createClass = makeStyles({
        textBox: { margin: "1% 0", width: 400 },
        headline: { fontSize: 30 }
    })
    const classes = createClass();
    const theme = createMuiTheme({
        palette: {
            secondary: { main: lightBlue[600] },
            primary: { main: orange[500] }
        }
    })
    return (
        <ThemeProvider theme={theme} >
            <div className="ContactUs">
                <h1 className="headline">הקלידי את פרטייך</h1>
                <TextField className={classes.textBox} variant="filled" label="תעודת זהות"></TextField><br />
                <TextField className={classes.textBox} variant="filled" label="שם פרטי" ></TextField><br />
                <TextField className={classes.textBox} variant="filled" label="שם משפחה"></TextField><br />
                <TextField className={classes.textBox} variant="filled" label="שנות וותק" ></TextField><br />
                {/* <TextField className={classes.textBox} variant="filled" label="סיסמה"></TextField><br /> */}
                <TextField className={classes.textBox} variant="filled" label="מייל" ></TextField><br />
                <TextField className={classes.textBox} variant="filled" label="סלולרי" ></TextField><br />
                <TextField className={classes.textBox} variant="filled" label="מספר משמרות בשבוע" ></TextField><br />
                <FormControlLabel control={<Checkbox defaultChecked />} label="יכול/ה להיות מנהל/ת " />
                <ButtonGroup variant="contained">
                    <Button color="primary">שמור</Button>
                </ButtonGroup>
            </div>
        </ThemeProvider>
    );
}

export default ContactUs;
