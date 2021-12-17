import * as React from "react";
import { useNavigate } from "react-router-dom";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { getUser } from "./getUser";
import axios from 'axios';

function Copyright(props) {
  return (
    <Typography
      variant="body2"
      color="text.secondary"
      align="center"
      {...props}
    >
      {"Copyright © "}
      <Link color="inherit" href="https://localhost:44337/">
        My Website
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
}

const theme = createTheme();

export default function EditCurrentUser() {
  const [user, setUser] = React.useState({
    userName: '',
    email: '',
    firstName: '',
    lastName: '',
  });
  const navigate = useNavigate();


  const handleSubmit = async (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    // eslint-disable-next-line no-console
    const requestData = {
      userName: data.get("Username"),
      email: data.get("Email"),
      firstName: data.get("FirstName"),
      lastName: data.get("LastName"),
    };
    console.log("request data: "+data);
    const response = await fetch(process.env.REACT_APP_API + "account/UpdateUserInfo", {
      method: "PUT",
      body: JSON.stringify(requestData),
      headers: {
        "Content-Type": "application/json",
      },
    });

    const result = response.json();
    console.log(result);

    console.log(response.status);
  };

  React.useEffect(() => { 
    axios.get(process.env.REACT_APP_API + "account/GetUserInfo")
        .then(res => {            
            setUser(res.data)
         })
         .catch(error=>{
             console.log("Error")
         })
  }, [])


   const onChangeUsername  = async (event) => {
  setUser({ userName: event.target.value });
};

const onChangeEmail  = async (event) => {
  setUser({ email: event.target.value });
};

const onChangeFirstname  = async (event) => {
  setUser({ firstName: event.target.value });
};

const onChangeLastname  = async (event) => {
  setUser({ lastName: event.target.value });
};
  return (
    <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <Typography component="h1" variant="h5">
            Edit Profile
          </Typography>
          <Box
            component="form"
            onSubmit={handleSubmit}
            noValidate
            sx={{ mt: 1 }}
          >
            <TextField
            onChange={onChangeUsername}
             value={user.userName}
              margin="normal"
              required
              fullWidth
              id="Username"
              label="UserName"
              name="Username"
              autoComplete="Username"
              autoFocus
            />
            <TextField
            onChange={onChangeEmail}
             value={user.email}
              margin="normal"
              required
              fullWidth
              id="Email"
              label="Email"
              name="Email"
              autoComplete="Email"
              autoFocus
            />
            <TextField
            onChange={onChangeFirstname}
             value={user.firstName}
              margin="normal"
              required
              fullWidth
              id="FirstName"
              label="FirstName"
              name="FirstName"
              autoComplete="FirstName"
              autoFocus
            />
            <TextField
            onChange={onChangeLastname}
             value={user.lastName}
              margin="normal"
              required
              fullWidth
              id="LastName"
              label="LastName"
              name="LastName"
              autoComplete="LastName"
              autoFocus
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Edit
            </Button>
          </Box>
        </Box>
        <Copyright sx={{ mt: 8, mb: 4 }} />
      </Container>
    </ThemeProvider>
  );
}
