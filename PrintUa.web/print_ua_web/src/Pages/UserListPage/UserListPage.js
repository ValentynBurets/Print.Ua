import './TemplateStyle.css'
import Button from 'react-bootstrap/Button'
import React from 'react'
import { useState } from 'react'
import { AddUserModalWindow } from './AddUser/ModalWindow'
import styled from 'styled-components'
import { makeStyles } from '@material-ui/core/styles'
//import Paper from '@material-ui/core/Paper'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
//import TableHead from '@material-ui/core/TableHead'
import TablePagination from '@material-ui/core/TablePagination'
import TableRow from '@material-ui/core/TableRow'
//import Container from '@material-ui/core/Container'
import TextData from '../../jsonData/English/UserList.json'

const columns = [
  {
    id: 'firstName',
    label: 'First Name',
    minWidth: 100,
    align: 'left',
    format: (value) => value.toLocaleString('en-US'),
  },
  {
    id: 'lastName',
    label: 'Last Name',
    minWidth: 100,
    align: 'left',
    format: (value) => value.toLocaleString('en-US'),
  },
  {
    id: 'email',
    label: 'Email',
    minWidth: 100,
    align: 'left',
    format: (value) => value.toLocaleString('en-US'),
  },
  {
    id: 'position',
    label: 'Position',
    minWidth: 100,
    align: 'left',
    format: (value) => value.toLocaleString('en-US'),
  },
]

const useStyles = makeStyles({
  root: {
    width: '100%',
  },
  container: {
    maxHeight: 440,
  },
})

function createData(firstName, lastName, email, position) {
  //some code for creating data
  //const density = population / size;
  return { firstName, lastName, email, position }
}

const rows = [
  createData('Ivan', 'Ivanov', 'Ivan.Ivanov@gmail.com', 'manager'),
  createData('Petro', 'Petrov', 'Petro.Petrov@gmail.com', 'user'),
  createData('Stepan', 'Stepanov', 'Stepan.Stepanov@gmail.com', 'worker'),
  createData('Olexiy', 'Olexiev', 'Olexiy.Olexiev@gmail.com', 'user'),
  createData('Fomka', 'Fomkovin', 'Fomka.Fomkovin@gmail.com', 'user'),
  createData('Furry', 'Furriev', 'Furry.Furriev@gmail.com', 'worker'),
]

const ModalWrapper = styled.div`
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 800px;
  height: 500px;
  box-shadow: 0 5px 16px rgba(0, 0, 0, 0.2);
  background: #fff;
  color: #000;
  display: grid;
  position: relative;
  z-index: 10;
  border-radius: 10px;
`
const ModalContent = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  line-height: 1.8;
  color: #141414;

  p {
    margin-bottom: 1rem;
  }

  button {
    padding: 10px 24px;
    background: #141414;
    color: #fff;
    border: none;
  }
`

function UserListPage() {
  const [showModal, setShowModal] = useState(false)

  const openModal = () => {
    setShowModal((prev) => !prev)
  }

  const [newUser, setNewUser] = useState({
    firstName: '',
    lastName: '',
    position: '',
    email: '',
    password: '',
  })

  const addUser = (form) => {
    setNewUser(form)
    rows.push(
      createData(form.firstName, form.lastName, form.email, form.position)
    )
    alert('new user added')
    console.log(
      'Parameters were got' +
        '\n password: ' +
        newUser.firstName +
        '\n lastName: ' +
        newUser.lastName +
        '\n position: ' +
        newUser.position +
        '\n email: ' +
        newUser.email +
        '\npassword: ' +
        newUser.password
    )
  }

  const classes = useStyles()
  const [page, setPage] = React.useState(0)
  const [rowsPerPage, setRowsPerPage] = React.useState(10)

  const handleChangePage = (event, newPage) => {
    setPage(newPage)
  }

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(+event.target.value)
    setPage(0)
  }

  return (
    <ModalWrapper>
      <ModalContent>
        <h1>{TextData.Header}</h1>
      </ModalContent>

      <TableContainer className={classes.container}>
        <Table stickyHeader aria-label='sticky table'>
          <TableRow>
            {columns.map((column) => (
              <TableCell
                key={column.id}
                align={column.align}
                style={{ minWidth: column.minWidth }}
              >
                {column.label}
              </TableCell>
            ))}
          </TableRow>

          <TableBody>
            {rows
              .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              .map((row) => {
                return (
                  <TableRow hover role='checkbox' tabIndex={-1} key={row.code}>
                    {columns.map((column) => {
                      const value = row[column.id]
                      return (
                        <TableCell key={column.id} align={column.align}>
                          {column.format && typeof value === 'number'
                            ? column.format(value)
                            : value}
                        </TableCell>
                      )
                    })}
                  </TableRow>
                )
              })}
          </TableBody>
        </Table>
      </TableContainer>

      <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component='div'
        count={rows.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onChangePage={handleChangePage}
        onChangeRowsPerPage={handleChangeRowsPerPage}
      />
      <ModalContent>
        <Button className='AddUserButton' onClick={openModal}>
          {TextData.AddUserButtonText}
        </Button>
        <br />
        <AddUserModalWindow
          showModal={showModal}
          setShowModal={setShowModal}
          addUser={addUser}
        />
      </ModalContent>
    </ModalWrapper>
  )
}

export default UserListPage
