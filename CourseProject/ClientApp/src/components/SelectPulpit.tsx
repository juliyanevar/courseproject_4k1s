import React, {useEffect} from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import axios from 'axios';

const api = axios.create({
    baseURL: process.env.REACT_APP_API+'pulpit/'
  })
  
  const useStyles = makeStyles((theme: Theme) =>
    createStyles({
      formControl: {
        margin: theme.spacing(1),
        minWidth: 200,
      },
      selectEmpty: {
        marginTop: theme.spacing(2),
      },
    }),
  );

export default function SelectFaculty() {
    const classes = useStyles();
  const [faculty, setFaculty] = React.useState<string>('');
  const [faculties, setFaculties] = React.useState<string[]>([]);

  const [pulpit, setPulpit] = React.useState<string>('');
  const [pulpits, setPulpits] = React.useState<string[]>([]);
  
  const handleChangeFaculty = (event: React.ChangeEvent<{value : unknown}>) => {
    setFaculty(event.target.value as string);
    fetchDataPulpit(event.target.value as string);
  };

  const handleChangPulpit = (event: React.ChangeEvent<{value : unknown}>) => {
    setPulpit(event.target.value as string);
  };

  async function fetchDataPulpit(faculty1:string) {
      try {
          console.log({FacultyName:faculty1});
        const res = await api.get("GetPulpitNamesByFaculty?facultyName="+faculty1);
        console.log(res.data);
        setPulpits(res.data);
      } catch (err) {
        console.log(err);
      }
    }


  useEffect( () => { 
    async function fetchDataFaculty() {
      try {
        const res = await api.get("GetFacultyNames");
        
        setFaculties(res.data);
      } catch (err) {
        console.log(err);
      }
    }     
     
    fetchDataFaculty();
    }, []);

  return (
    <Box sx={{ minWidth: 120 }}>
        <FormControl variant="filled" fullWidth className={classes.formControl}>
        <InputLabel id="select-faculty-label1">Faculty</InputLabel>
        <Select
          labelId="select-faculty-label1"
          id="select-faculty1"
          value={faculty}
          onChange={handleChangeFaculty}
        >
          {faculties.map((faculty) => (
            <MenuItem
              key={faculty}
              value={faculty}
            >
              {faculty}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      <br/>
       <FormControl variant="filled" fullWidth className={classes.formControl}>
        <InputLabel id="select-pulpit-label">Pulpit</InputLabel>
        <Select
          labelId="select-pulpit-label"
          id="select-pulpit"
          name = "select-pulpit"
          value={pulpit}
          onChange={handleChangPulpit}
        >
          {pulpits.map((pulpit) => (
            <MenuItem
              key={pulpit}
              value={pulpit}
            >
              {pulpit}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Box>
  );
}