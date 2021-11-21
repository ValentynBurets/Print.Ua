import React, { useState } from 'react'
import Radio from '@material-ui/core/Radio'
import RadioGroup from '@material-ui/core/RadioGroup'
import FormControlLabel from '@material-ui/core/FormControlLabel'
import FormControl from '@material-ui/core/FormControl'
import FormLabel from '@material-ui/core/FormLabel'
import TextField from '@material-ui/core/TextField'
import TextData from '../../../jsonData/English/UserList.json'


function AddUserForm({ addUserModal }) {
  const [form, setForm] = useState({
    firstName: '',
    lastName: '',
    position: '',
    email: '',
    password: '',
  })

  const [errors, setErrors] = useState({})

  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    })
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    if (handleValidation() === true) {
      console.log(
        'Parameters were got' +
          '\n password: ' +
          form.firstName +
          '\n lastName: ' +
          form.lastName +
          '\n position: ' +
          form.position +
          '\n email: ' +
          form.email +
          '\npassword: ' +
          form.password
      )
      addUserModal(form)
    }
  }

  const handleValidation = () => {
    let errors = {}
    let isFormValid = true

    if (!form.firstName) {
      isFormValid = false
      errors.firstName = 'first name cannot be empty!'
    } else if (typeof form.firstName !== undefined) {
      if (!form.firstName.match(/^[a-zA-Z ]+$/)) {
        isFormValid = false
        errors.firstName = 'first name can only include letters!'
      }
    }

    if (!form.lastName) {
      isFormValid = false
      errors.lastName = 'last name cannot be empty!'
    } else if (typeof form.lastName !== undefined) {
      if (!form.lastName.match(/^[a-zA-Z ]+$/)) {
        isFormValid = false
        errors.lastName = 'last name can only include letters!'
      }
    }

    if (!form.position) {
      isFormValid = false
      errors.position = 'position cannot be empty!'
    } else if (typeof form.position !== undefined) {
      if (!form.position.match(/^[a-zA-Z ]+$/)) {
        isFormValid = false
        errors.position = 'position can only include letters!'
      }
    }

    if (!form.email) {
      isFormValid = false
      errors.email = 'Email cannot be empty!'
    } else if (typeof (form.email !== undefined)) {
      if (!form.email.match(/\S+@\S+\.\S+/)) {
        isFormValid = false
        errors.email = 'Email must be in the format example@example.com'
      }
    }

    if (!form.password) {
      isFormValid = false
      errors.password = 'password cannot be empty!'
    } else if (typeof (form.password !== undefined)) {
      if (!form.password.match()) {
        isFormValid = false
        errors.password = 'password must be in the format SomePass'
      }
    }

    setErrors(errors)
    return isFormValid
  }

  return (
    <div>
      <h2 style={{ textAlign: 'center' }}>{TextData.AddUserButtonText}</h2>
      <form onSubmit={handleSubmit}>
        <TextField
          required
          id='firstNameInput'
          name='firstName'
          label='First name:'
          defaultValue='name'
          placeholder='Please type your first name'
          value={form.firstName}
          onChange={(e) => handleChange(e)}
          helperText={errors.firstName}
        />
        <br />
        <TextField
          required
          id='lastNameInput'
          name='lastName'
          label='Last name:'
          defaultValue='last name'
          placeholder='Please type your last name'
          value={form.lastName}
          onChange={(e) => handleChange(e)}
          helperText={errors.lastName}
        />
        <br />
        <TextField
          required
          id='emailInput'
          name='email'
          label='email:'
          defaultValue='email'
          placeholder='Please type your email'
          value={form.email}
          onChange={(e) => handleChange(e)}
          helperText={errors.email}
        />
        <br />
        <TextField
          required
          id='passwordInput'
          name='password'
          label='password:'
          defaultValue='password'
          placeholder='Please type your password'
          value={form.password}
          onChange={(e) => handleChange(e)}
          helperText={errors.password}
        />
        <br />
        <FormControl margin='normal' component='fieldset'>
          <FormLabel component='legend'>{TextData.Position}</FormLabel>
          <RadioGroup
            aria-label='position'
            name='position'
            value={form.position}
            onChange={handleChange}
          >
            <FormControlLabel
              value='manager'
              control={<Radio />}
              label='Manager'
            />
            <FormControlLabel
              value='worker'
              control={<Radio />}
              label='Worker'
            />
            <FormControlLabel value='user' control={<Radio />} label='User' />
            <FormControlLabel value='other' control={<Radio />} label='Other' />
          </RadioGroup>
        </FormControl>

        <span>{errors.position}</span>

        <br></br>
        <button>{TextData.AddUserButtonText}</button>
      </form>
    </div>
  )
}

export default AddUserForm
