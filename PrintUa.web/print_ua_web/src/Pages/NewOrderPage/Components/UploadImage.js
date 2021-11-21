import { 
    React, 
    useState
  } from "react";
import PropTypes from 'prop-types'
import picture from '../../../Components/Images/picture.png'
import encodeFile from "../Services/encodeFile"
import './style.sass'

const UploadImage = (props) => {
  //const [file, setFile] = useState(null);
  const onFileChange = (e) => {
    encodeFile(e.target.files[0], props.setFileString);
    //let image = URL.createObjectURL(e.target.files[0]);
    //setFile(image)
  };

  return (
    <div className="PictureBox">
      <div>
          {(props.fileString === "" || props.fileString == null) 
            ? (
                <img className='imageStyle' 
                  src={picture}
                  alt={picture}
                />
              )
            : (
                <img className='imageStyle'
                  src={"data:image/jpg;base64," + props.fileString}  
                  alt={picture}
                />
              )
          } 
      </div>
      <input  type="file" id="input" onChange={onFileChange} />
    </div>
  );
};

UploadImage.protoTypes = {
  setFileString: PropTypes.string
}

export default UploadImage;