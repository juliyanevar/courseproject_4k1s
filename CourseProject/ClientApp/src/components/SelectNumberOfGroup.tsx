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

export default function SelectNumberOfGroup() {
    const classes = useStyles();
  const [group, setGroup] = React.useState<string>('');
  const [groups, setGroups] = React.useState<string[]>([]);
  
  const handleChange = (event: React.ChangeEvent<{value : unknown}>) => {
    setGroup(event.target.value as string)
  };

  useEffect( () => { 
    async function fetchData() {
      try {
        const res = await api.get("getNumberOfGroup");
        console.log(res.data);
        setGroups(res.data);
      } catch (err) {
        console.log(err);
      }
    }
    fetchData();
    }, []);

  return (
    <Box sx={{ minWidth: 120 }}>
        <FormControl variant="filled" fullWidth className={classes.formControl}>
        <InputLabel id="select-group-label">Group</InputLabel>
        <Select
          labelId="select-group-label"
          id="select-group"
          name="select-group"
          value={group}
          onChange={handleChange}
        >
          {groups.map((group) => (
            <MenuItem
              key={group}
              value={group}
            >
              {group}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Box>
  );
}