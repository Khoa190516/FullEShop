import { Modal } from "antd";
import { GenericModalProps } from "../../Models/Generic/GenericModalProps";

const GenericModal = ({
  isOpen,
  onClose,
  childComponent,
  footer,
  title,
  style,
}: GenericModalProps) => {

  return (
    <Modal
      title={title}
      destroyOnClose={true}
      style={style}
      open={isOpen}
      onOk={onClose}
      onCancel={onClose}
      maskClosable={false}
      width="max-content"
      footer={footer}
    >      
        {childComponent}
    </Modal>
  );
};

export default GenericModal;
