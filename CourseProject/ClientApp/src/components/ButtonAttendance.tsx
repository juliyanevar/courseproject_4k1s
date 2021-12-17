import React, { useState, useEffect } from "react";
import Button from "@material-ui/core/Button";
import {useLocation, useNavigate} from "react-router-dom";

export default function ButtonAttendance() {
    const navigate = useNavigate();
  const search = useLocation().search;

  const SubjectId = new URLSearchParams(search).get('subjectId');
  const AuditoriumName = new URLSearchParams(search).get('AuditoriumName');
  const DateTime = new URLSearchParams(search).get('Date');
  const TeacherName = new URLSearchParams(search).get('teacher');


  const [lat, setLat] = useState(Number);
    const [lon, setLon] = useState(Number);
    const [iserror, setIserror] = useState(false);
    const [errorMessages, setErrorMessages] = useState(['']);

    async function checkGeolocation() {
        let errorList = [];
        if (localStorage.getItem('username') != null) {
            navigator.geolocation.getCurrentPosition(position => {
                setLat(position.coords.latitude);
                setLon(position.coords.longitude);
            });
            if (lat < 53.897 && lat > 53.89 && lon < 27.563 && lon > 27.55) {
                const requestData = {
                    DateTime: DateTime,
                    SubjectId: SubjectId,
                    AuditoriumName: AuditoriumName,
                    TeacherUsername: TeacherName,
                };
                console.log(requestData);
                const response = await fetch(process.env.REACT_APP_API + "attendance/Create", {
                    method: "POST",
                    body: JSON.stringify(requestData),
                    headers: {
                        "Content-Type": "application/json",
                    },
                })
            }
            else {
                setErrorMessages(["You are not at the University!"]);
                setIserror(true);
            }
        }
        else {
            navigate("/SignIn");
        }
        
    }

  return (
    <div className="d-grid gap-2 text-center">
      <Button onClick={checkGeolocation} variant="contained" color="primary" size="large">
        I'm here!
          </Button>
          <h1>{errorMessages.map((msg, i) => {
              return <div key={i}>{msg}</div>
          })}</h1>
    </div>
  );
}
