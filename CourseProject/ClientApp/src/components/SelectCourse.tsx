import React, {useEffect} from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import axios from 'axios';

const api = axios.create({
    baseURL: process.env.REACT_APP_API+'group/'
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

export default function SelectCourse() {
    const classes = useStyles();
  const [course, setCourse] = React.useState<string>('');
  const [courses, setCourses] = React.useState<string[]>([]);
  
  const handleChange = (event: React.ChangeEvent<{value : unknown}>) => {
    setCourse(event.target.value as string)
  };

  useEffect( () => { 
    async function fetchData() {
      try {
        const res = await api.get("getCourses");
        console.log(res.data);
        setCourses(res.data);
      } catch (err) {
        console.log(err);
      }
    }
    fetchData();
    }, []);

  return (
    <Box sx={{ minWidth: 120 }}>
        <FormControl variant="filled" fullWidth className={classes.formControl}>
        <InputLabel id="select-course-label">Course</InputLabel>
        <Select
          labelId="select-course-label"
          id="select-course"
          value={course}
          onChange={handleChange}
        >
          {courses.map((course) => (
            <MenuItem
              key={course}
              value={course}
            >
              {course}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Box>
  );
}