import axios from "axios";
import { stringify } from "querystring";
import React, { useState, useEffect } from "react";
import Checkboxes from './changeTry';
import { useNavigate } from 'react-router-dom'; 

interface DAY {
  id: number;
  name: string;
}

const CheckTry: React.FunctionComponent = () => {
  let navigate = useNavigate()
  const days: DAY[] = [{ id: 1, name: 'ראשון' }, { id: 2, name: 'שני' }, { id: 3, name: 'שלישי' }, { id: 4, name: 'רביעי' }, { id: 5, name: 'חמישי' }, { id: 6, name: 'שישי' }, { id: 7, name: 'שבת' }]
  const [ids, setIds] = useState<Array<number>>([]);

  let result = [0];
  const selectUser = (event: React.ChangeEvent<HTMLInputElement>) => {
    const selectedId = parseInt(event.target.value);
    result.push(selectedId);
  };

  const removeUsers = async () => {
    console.log(result);
    result = result.filter(item => item != 0)
    //let request = axios.post("https://localhost:44336/api/Employee/createDynamicColum", result)
    //let x = await request;
    localStorage.setItem("arrResultDays", JSON.stringify(result));
    const remainingUsers: DAY[] = days.filter(
      (user) => !ids.includes(user.id)
    );
    navigate('/Checkboxes');
  };

  return (
    <div style={styles.container}>
      <h2>בחר ימים לשיבוץ</h2>
      {days.length === 0 && <h3>Loading...</h3>}
      {days.length > 0 &&
        days.map((day) => (

          <div style={styles.userItem} key={day.id}>
            <span style={styles.userId}>{day.id}</span>
            <span style={styles.userName}>{day.name}</span>
            <span style={styles.userCheckbox}></span>
            <input
              type="checkbox"
              value={day.id}
              onChange={selectUser}
            />
          </div>
        ))}

      <button style={styles.button} onClick={removeUsers}>
         שלח
      </button>
    </div>
  );
};

// Styling
const styles: { [key: string]: React.CSSProperties } = {
  container: {
    width: 500,
    margin: "10px auto",
    display: "flex",
    flexDirection: "column",
  },
  userItem: {
    width: "100%",
    display: "flex",
    justifyContent: "space-between",
    margin: "6px 0",
    padding: "8px 15px",
    backgroundColor: "#fcf9c4",
  },
  userId: {
    width: "5%",
  },
  userName: {
    width: "30%",
  },
  userEmail: {
    width: "40%",
  },
  button: {
    marginTop: 30,
    padding: "15px 30px",
    backgroundColor: "red",
    color: "white",
    fontWeight: "bold",
    border: "none",
    cursor: "pointer",
  },
};

export default CheckTry;