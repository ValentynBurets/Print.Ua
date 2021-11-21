import { Container} from 'react-bootstrap'
import CommentCard from './CommentCard'

export default function CommentDeck(props){
    return(
        <Container>
            {props.comments?.map((comment) => (
                <CommentCard 
                    key={comment.id} 
                    comment={comment}/>
            ))}
        </Container>
    )   
}
