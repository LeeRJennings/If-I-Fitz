import { useState, useEffect } from "react"
import { updateComment, getCommentById } from "../../modules/commentManager";
import { useNavigate, useParams } from "react-router-dom"
import { Button,Form,FormGroup,Input,Label } from 'reactstrap';


export const CommentEdit = () => {
    const [comment, setComment] = useState({
        content: ""
    })
    const [isLoading, setIsLoading] = useState(true)

    const navigate = useNavigate()
    const {id} = useParams()

    const getComment = () => {
        getCommentById(id)
        .then(comment => setComment(comment))
    }

    const handleFieldChange = (e) => {
        const newComment = {...comment}
        let selectedVal = e.target.value
        newComment[e.target.id] = selectedVal
        setComment(newComment)
    }

    const handleClickSave = () => {
        if (comment.content === "") {
            window.alert("A comment with no content seems pretty pointless ........")
        } else {
            setIsLoading(true)
            delete comment.userProfile
            updateComment(comment)
            .then(() => navigate(`/posts/${comment.postId}`))
        }
    }

    useEffect(() => {
        getComment()
        setIsLoading(false)
    }, [])

    return (
        <Form>
            <h2>Add a New Comment </h2>
            <FormGroup>
                <Input type="textarea" 
                        name="content" 
                        id="content"
                        onChange={handleFieldChange}
                        value={comment.content}
                        placeholder="Write your comment here..." />
            </FormGroup>
            <Button color="primary" onClick={() => handleClickSave()} disabled={isLoading}>Save Edits</Button>
            <Button onClick={() => navigate(-1)}>Cancel</Button>
        </Form>
    )
}