import { Col, Container, Button, Card} from 'react-bootstrap'
import Table from 'react-bootstrap/Table'
import TheaderListOrder from './TheaderListOrder'
import Tbody from './Tbody'

function ProductList(props){

    return(
      <Col xs={11} sm={11} md={6} lg={5}>
        <Container className='ProductList'>
        <Table borderless>
                <TheaderListOrder/>
                <Tbody 
                    bodyData={props}
                />
            </Table>
            </Container>
      </Col> 
  )
}

export default ProductList