using System;
using System.Collections;
using System.Net.Mail;
using System.Text;

namespace EIRS.Common
{
    public class EmailHandler
    {
        public static bool SendEmail(string p_strEmailRecepientName, string p_strEmailRecepientEmail, string p_strEmailSubject, bool p_blnEmailBodyHtml, string p_strEmailContent, ArrayList p_arlEmailAttachments)
        {
            try
            {
                string strEmailSenderName = GlobalDefaultValues.EmailSenderName;
                string strEmailSenderEmail = GlobalDefaultValues.EmailSenderEmail;

                SmtpClient scEmailSender = new SmtpClient();
                MailMessage mmEmailContainer = new MailMessage
                {
                    From = new MailAddress(strEmailSenderEmail, strEmailSenderName)
                };

                StringBuilder sbReceipient = new StringBuilder();
                sbReceipient.Append(p_strEmailRecepientName.Replace(".", " "));
                sbReceipient.Append(" <");
                sbReceipient.Append(p_strEmailRecepientEmail);
                sbReceipient.Append(">");
                mmEmailContainer.To.Add(sbReceipient.ToString());

                mmEmailContainer.Subject = p_strEmailSubject;
                mmEmailContainer.IsBodyHtml = p_blnEmailBodyHtml;
                mmEmailContainer.Body = p_strEmailContent;

                if (p_arlEmailAttachments != null)
                {
                    if (p_arlEmailAttachments.Count > 0)
                    {
                        foreach (Attachment at in p_arlEmailAttachments)
                        {
                            mmEmailContainer.Attachments.Add(at);
                        }
                    }
                }


                scEmailSender.DeliveryMethod = SmtpDeliveryMethod.Network;
                scEmailSender.Send(mmEmailContainer);
            }
            catch (Exception Ex)
            {
                return false;
            }

            return true;
        }

        public static bool SendEmail(string p_strSenderName, string p_strSenderEmail, string p_strEmailRecepientName, string p_strEmailRecepientEmail, string p_strEmailSubject, bool p_blnEmailBodyHtml, string p_strEmailContent, ArrayList p_arlEmailAttachments)
        {
            try
            {
                string strEmailSenderName = p_strSenderName;
                string strEmailSenderEmail = p_strSenderEmail;

                SmtpClient scEmailSender = new SmtpClient();
                MailMessage mmEmailContainer = new MailMessage
                {
                    From = new MailAddress(strEmailSenderEmail, strEmailSenderName)
                };

                StringBuilder sbReceipient = new StringBuilder();
                sbReceipient.Append(p_strEmailRecepientName.Replace(".", " "));
                sbReceipient.Append(" <");
                sbReceipient.Append(p_strEmailRecepientEmail);
                sbReceipient.Append(">");
                mmEmailContainer.To.Add(sbReceipient.ToString());

                mmEmailContainer.Subject = p_strEmailSubject;
                mmEmailContainer.IsBodyHtml = p_blnEmailBodyHtml;
                mmEmailContainer.Body = p_strEmailContent;

                if (p_arlEmailAttachments != null)
                {
                    if (p_arlEmailAttachments.Count > 0)
                    {
                        foreach (Attachment at in p_arlEmailAttachments)
                        {
                            mmEmailContainer.Attachments.Add(at);
                        }
                    }
                }

                scEmailSender.DeliveryMethod = SmtpDeliveryMethod.Network;
                scEmailSender.Send(mmEmailContainer);
            }
            catch (Exception Ex)
            {
                return false;
            }

            return true;
        }
    }
}
