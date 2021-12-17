import React, {useEffect} from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import axios from 'axios';

const api = axios.create({
    baseURL: process.env.REACT_APP_API+'auditorium/'
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

export default function GetAuditoriumNames() {
    const classes = useStyles();
  const [auditorium, setAuditorium] = React.useState<string>('');
  const [auditoriums, setAuditoriums] = React.useState<string[]>([]);
  
  const handleChange = (event: React.ChangeEvent<{value : unknown}>) => {
    setAuditorium(event.target.value as string)
  };

  useEffect( () => { 
    async function fetchData() {
      try {
        const res = await api.get("GetAuditoriumNames");
        console.log(res.data);
        setAuditoriums(res.data);
      } catch (err) {
        console.log(err);
      }
    }
    fetchData();
    }, []);

  return (
    <Box sx={{ minWidth: 400 }}>
        <FormControl variant="filled" fullWidth className={classes.formControl}>
        <InputLabel id="select-auditorium-label">Auditorium</InputLabel>
        <Select
          labelId="select-auditorium-label"
          id="select-auditorium"
          name="select-auditorium"
          value={auditorium}
          onChange={handleChange}
        >
          {auditoriums.map((auditorium) => (
            <MenuItem
              key={auditorium}
              value={auditorium}
            >
              {auditorium}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Box>
  );
}