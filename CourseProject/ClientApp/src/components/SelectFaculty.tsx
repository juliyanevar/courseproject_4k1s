import React, {useEffect} from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import axios from 'axios';

const api = axios.create({
    baseURL: process.env.REACT_APP_API+'profession/'
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

  const [profession, setProfession] = React.useState<string>('');
  const [professions, setProfessions] = React.useState<string[]>([]);
  
  const handleChangeFaculty = (event: React.ChangeEvent<{value : unknown}>) => {
    setFaculty(event.target.value as string);
    fetchDataProfession(event.target.value as string);
  };

  const handleChangProfession = (event: React.ChangeEvent<{value : unknown}>) => {
    setProfession(event.target.value as string);
  };

  async function fetchDataProfession(faculty1:string) {
      try {
          console.log({FacultyName:faculty1});
        const res = await api.get("GetProfessionNamesByFaculty?facultyName="+faculty1);
        console.log(res.data);
        setProfessions(res.data);
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
        <InputLabel id="select-faculty-label">Faculty</InputLabel>
        <Select
          labelId="select-faculty-label"
          id="select-faculty"
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
        <InputLabel id="select-profession-label">Profession</InputLabel>
        <Select
          labelId="select-profession-label"
          id="select-profession"
          name = "select-profession"
          value={profession}
          onChange={handleChangProfession}
        >
          {professions.map((profession) => (
            <MenuItem
              key={profession}
              value={profession}
            >
              {profession}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Box>
  );
}