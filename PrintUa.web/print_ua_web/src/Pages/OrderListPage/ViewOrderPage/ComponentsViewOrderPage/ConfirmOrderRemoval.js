import text from '../../../../jsonData/English/ViewOrderPage.json'
import {Modal, Button} from 'react-bootstrap'

function ModalConfirmRemoval(props) {

    return (
      <Modal
        {...props}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            {text.ConfirmOrderCancel}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>
          {text.ConfirmOrderCancelText}
          </p>
        </Modal.Body>
        <Modal.Footer>
          <Button onClick={props.onHide} className="btn-secondary">{text.Cancel}</Button>
          <Button onClick={props.onAccept} className="btn-primary" id="DiscardButton">{text.Continue}</Button>
        </Modal.Footer>
      </Modal>
    );
  }

  export default ModalConfirmRemoval