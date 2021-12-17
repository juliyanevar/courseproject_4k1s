import React, { useState, useEffect } from "react";
import { forwardRef } from "react";
import Grid from "@material-ui/core/Grid";
import { alpha } from "@material-ui/core/styles";

import MaterialTable from "material-table";
import AddBox from "@material-ui/icons/AddBox";
import ArrowDownward from "@material-ui/icons/ArrowDownward";
import Check from "@material-ui/icons/Check";
import ChevronLeft from "@material-ui/icons/ChevronLeft";
import ChevronRight from "@material-ui/icons/ChevronRight";
import Clear from "@material-ui/icons/Clear";
import DeleteOutline from "@material-ui/icons/DeleteOutline";
import Edit from "@material-ui/icons/Edit";
import FilterList from "@material-ui/icons/FilterList";
import FirstPage from "@material-ui/icons/FirstPage";
import LastPage from "@material-ui/icons/LastPage";
import Remove from "@material-ui/icons/Remove";
import SaveAlt from "@material-ui/icons/SaveAlt";
import Search from "@material-ui/icons/Search";
import ViewColumn from "@material-ui/icons/ViewColumn";
import axios from "axios";
import Alert from "@material-ui/lab/Alert";

const tableIcons = {
  Add: forwardRef((props, ref) => <AddBox {...props} ref={ref} />),
  Check: forwardRef((props, ref) => <Check {...props} ref={ref} />),
  Clear: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
  Delete: forwardRef((props, ref) => <DeleteOutline {...props} ref={ref} />),
  DetailPanel: forwardRef((props, ref) => (
    <ChevronRight {...props} ref={ref} />
  )),
  Edit: forwardRef((props, ref) => <Edit {...props} ref={ref} />),
  Export: forwardRef((props, ref) => <SaveAlt {...props} ref={ref} />),
  Filter: forwardRef((props, ref) => <FilterList {...props} ref={ref} />),
  FirstPage: forwardRef((props, ref) => <FirstPage {...props} ref={ref} />),
  LastPage: forwardRef((props, ref) => <LastPage {...props} ref={ref} />),
  NextPage: forwardRef((props, ref) => <ChevronRight {...props} ref={ref} />),
  PreviousPage: forwardRef((props, ref) => (
    <ChevronLeft {...props} ref={ref} />
  )),
  ResetSearch: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
  Search: forwardRef((props, ref) => <Search {...props} ref={ref} />),
  SortArrow: forwardRef((props, ref) => <ArrowDownward {...props} ref={ref} />),
  ThirdStateCheck: forwardRef((props, ref) => <Remove {...props} ref={ref} />),
  ViewColumn: forwardRef((props, ref) => <ViewColumn {...props} ref={ref} />),
};

const api = axios.create({
  baseURL: process.env.REACT_APP_API + "account/",
});

function Teachers() {
  var columns = [
    { title: "Id", field: "id", hidden: true },
    { title: "Username", field: "userName"},
    { title: "Email", field: "email" },
    { title: "First Name", field: "firstName" },
    { title: "Last Name", field: "lastName" },
    { title: "Pulpit Name", field: "pulpitName" },
  ];
  const [data, setData] = useState([]); //table data

  //for error handling
  const [iserror, setIserror] = useState(false);
  const [errorMessages, setErrorMessages] = useState([]);

  useEffect(() => {
    api
      .get("getTeachers")
      .then((res) => {
        console.log(res.data);
        setData(res.data);
      })
      .catch((error) => {
        console.log("Error");
      });
  }, []);

  const handleRowUpdate = (newData, oldData, resolve) => {
    //validation
    let errorList = [];
    if (newData.pulpitName === "") {
      errorList.push("Please enter pulpit name");
    }

    if (errorList.length < 1) {
      api
        .put("/updateTeacher", {
          Id: oldData.id,
          pulpitName: newData.pulpitName
        })
        .then((res) => {
          const dataUpdate = [...data];
          const index = oldData.tableData.id;
          dataUpdate[index] = newData;
          setData([...dataUpdate]);
          resolve();
          setIserror(false);
          setErrorMessages([]);
        })
        .catch((error) => {
          setErrorMessages(["Update failed! Server error"]);
          setIserror(true);
          resolve();
        });
    } else {
      setErrorMessages(errorList);
      setIserror(true);
      resolve();
    }
  };

//   const handleRowAdd = (newData, resolve) => {
//     //validation
//     let errorList = [];
//     if (newData.dateTime === "") {
//       errorList.push("Please enter date");
//     }
//     if (newData.subjectName === "") {
//       errorList.push("Please enter subject name");
//     }
//     if (newData.auditoriumName === "") {
//       errorList.push("Please enter auditorium name");
//     }
//     if (newData.teacherUserName === "") {
//       errorList.push("Please enter teacher username");
//     }
//     if (newData.studentUserName === "") {
//       errorList.push("Please enter student username");
//     }
//     console.log({
//           DateTime: newData.dateTime,
//           SubjectName: newData.subjectName,
//           AuditoriumName: newData.auditoriumName,
//           TeacherUserName: newData.teacherUserName,
//           StudentUserName: newData.studentUserName
//     });

//     if (errorList.length < 1) {
//       //no error
//       api
//         .post("create", {
//           DateTime: newData.dateTime,
//           SubjectName: newData.subjectName,
//           AuditoriumName: newData.auditoriumName,
//           TeacherUserName: newData.teacherUserName,
//           StudentUserName: newData.studentUserName
//         })
//         .then((res) => {
//           let dataToAdd = [...data];
//           dataToAdd.push(newData);
//           setData(dataToAdd);
//           resolve();
//           setErrorMessages([]);
//           setIserror(false);
//         })
//         .catch((error) => {
//           setErrorMessages(["Cannot add data. Server error!"]);
//           setIserror(true);
//           resolve();
//         });
//     } else {
//       setErrorMessages(errorList);
//       setIserror(true);
//       resolve();
//     }
//   };

  const handleRowDelete = (oldData, resolve) => {
    api
      .delete("Delete", {
        data: { id: oldData.id },
        headers: { "Content-Type": "application/json" },
      })
      .then((res) => {
        const dataDelete = [...data];
        const index = oldData.tableData.id;
        dataDelete.splice(index, 1);
        setData([...dataDelete]);
        resolve();
      })
      .catch((error) => {
        setErrorMessages(["Delete failed! Server error"]);
        setIserror(true);
        resolve();
      });
  };

  return (
    <div className="App">
      <Grid container spacing={1}>
        <Grid item xs={12}>
          <div>
            {iserror && (
              <Alert severity="error">
                {errorMessages.map((msg, i) => {
                  return <div key={i}>{msg}</div>;
                })}
              </Alert>
            )}
          </div>
          <MaterialTable
            title="Teachers list"
            columns={columns}
            data={data}
            icons={tableIcons}
            editable={{
              onRowUpdate: (newData, oldData) =>
                new Promise((resolve) => {
                  handleRowUpdate(newData, oldData, resolve);
                }),
            //   onRowAdd: (newData) =>
            //     new Promise((resolve) => {
            //       handleRowAdd(newData, resolve);
            //     }),
              onRowDelete: (oldData) =>
                new Promise((resolve) => {
                  handleRowDelete(oldData, resolve);
                }),
            }}
            options={{
              sorting: true,
              grouping: true,
            }}
          />
        </Grid>
      </Grid>
    </div>
  );
}

export default Teachers;
