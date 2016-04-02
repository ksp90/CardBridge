namespace BroadcastChat
{
    partial class GameSummary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbBiddingDetails = new System.Windows.Forms.GroupBox();
            this.lblColorCard = new System.Windows.Forms.Label();
            this.lblColorCardValue = new System.Windows.Forms.Label();
            this.lblYourBid = new System.Windows.Forms.Label();
            this.lblOponent1Bid = new System.Windows.Forms.Label();
            this.lblOponent2bid = new System.Windows.Forms.Label();
            this.lblOponent3Bid = new System.Windows.Forms.Label();
            this.gbScore = new System.Windows.Forms.GroupBox();
            this.gbCurrentScore = new System.Windows.Forms.GroupBox();
            this.lblYourCurrentScore = new System.Windows.Forms.Label();
            this.lblOponent1CurrentScore = new System.Windows.Forms.Label();
            this.lblOponent2CurrentScore = new System.Windows.Forms.Label();
            this.lblOponent3CurrentScore = new System.Windows.Forms.Label();
            this.gbTotalScore = new System.Windows.Forms.GroupBox();
            this.lblOponent3TotalScore = new System.Windows.Forms.Label();
            this.lblOponent2TotalScore = new System.Windows.Forms.Label();
            this.lblOponent1TotalScore = new System.Windows.Forms.Label();
            this.lblYourTotalScore = new System.Windows.Forms.Label();
            this.gbBiddingDetails.SuspendLayout();
            this.gbScore.SuspendLayout();
            this.gbCurrentScore.SuspendLayout();
            this.gbTotalScore.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbBiddingDetails
            // 
            this.gbBiddingDetails.Controls.Add(this.lblYourBid);
            this.gbBiddingDetails.Controls.Add(this.lblOponent1Bid);
            this.gbBiddingDetails.Controls.Add(this.lblOponent2bid);
            this.gbBiddingDetails.Controls.Add(this.lblOponent3Bid);
            this.gbBiddingDetails.Location = new System.Drawing.Point(12, 42);
            this.gbBiddingDetails.Name = "gbBiddingDetails";
            this.gbBiddingDetails.Size = new System.Drawing.Size(180, 122);
            this.gbBiddingDetails.TabIndex = 0;
            this.gbBiddingDetails.TabStop = false;
            this.gbBiddingDetails.Text = "Bidding Details";
            // 
            // lblColorCard
            // 
            this.lblColorCard.AutoSize = true;
            this.lblColorCard.Location = new System.Drawing.Point(12, 9);
            this.lblColorCard.Name = "lblColorCard";
            this.lblColorCard.Size = new System.Drawing.Size(65, 13);
            this.lblColorCard.TabIndex = 1;
            this.lblColorCard.Text = "Color Card : ";
            // 
            // lblColorCardValue
            // 
            this.lblColorCardValue.AutoSize = true;
            this.lblColorCardValue.Location = new System.Drawing.Point(83, 9);
            this.lblColorCardValue.Name = "lblColorCardValue";
            this.lblColorCardValue.Size = new System.Drawing.Size(22, 13);
            this.lblColorCardValue.TabIndex = 2;
            this.lblColorCardValue.Text = "NA";
            // 
            // lblYourBid
            // 
            this.lblYourBid.AutoSize = true;
            this.lblYourBid.Location = new System.Drawing.Point(15, 26);
            this.lblYourBid.Name = "lblYourBid";
            this.lblYourBid.Size = new System.Drawing.Size(44, 13);
            this.lblYourBid.TabIndex = 0;
            this.lblYourBid.Text = "YourBid";
            // 
            // lblOponent1Bid
            // 
            this.lblOponent1Bid.AutoSize = true;
            this.lblOponent1Bid.Location = new System.Drawing.Point(15, 50);
            this.lblOponent1Bid.Name = "lblOponent1Bid";
            this.lblOponent1Bid.Size = new System.Drawing.Size(22, 13);
            this.lblOponent1Bid.TabIndex = 1;
            this.lblOponent1Bid.Text = "NA";
            // 
            // lblOponent2bid
            // 
            this.lblOponent2bid.AutoSize = true;
            this.lblOponent2bid.Location = new System.Drawing.Point(15, 74);
            this.lblOponent2bid.Name = "lblOponent2bid";
            this.lblOponent2bid.Size = new System.Drawing.Size(22, 13);
            this.lblOponent2bid.TabIndex = 2;
            this.lblOponent2bid.Text = "NA";
            // 
            // lblOponent3Bid
            // 
            this.lblOponent3Bid.AutoSize = true;
            this.lblOponent3Bid.Location = new System.Drawing.Point(15, 98);
            this.lblOponent3Bid.Name = "lblOponent3Bid";
            this.lblOponent3Bid.Size = new System.Drawing.Size(22, 13);
            this.lblOponent3Bid.TabIndex = 3;
            this.lblOponent3Bid.Text = "NA";
            // 
            // gbScore
            // 
            this.gbScore.Controls.Add(this.gbTotalScore);
            this.gbScore.Controls.Add(this.gbCurrentScore);
            this.gbScore.Location = new System.Drawing.Point(198, 9);
            this.gbScore.Name = "gbScore";
            this.gbScore.Size = new System.Drawing.Size(333, 240);
            this.gbScore.TabIndex = 3;
            this.gbScore.TabStop = false;
            this.gbScore.Text = "Score Details";
            // 
            // gbCurrentScore
            // 
            this.gbCurrentScore.Controls.Add(this.lblYourCurrentScore);
            this.gbCurrentScore.Controls.Add(this.lblOponent1CurrentScore);
            this.gbCurrentScore.Controls.Add(this.lblOponent2CurrentScore);
            this.gbCurrentScore.Controls.Add(this.lblOponent3CurrentScore);
            this.gbCurrentScore.Location = new System.Drawing.Point(6, 20);
            this.gbCurrentScore.Name = "gbCurrentScore";
            this.gbCurrentScore.Size = new System.Drawing.Size(321, 100);
            this.gbCurrentScore.TabIndex = 0;
            this.gbCurrentScore.TabStop = false;
            this.gbCurrentScore.Text = "Current Score Summary";
            // 
            // lblYourCurrentScore
            // 
            this.lblYourCurrentScore.AutoSize = true;
            this.lblYourCurrentScore.Location = new System.Drawing.Point(14, 28);
            this.lblYourCurrentScore.Name = "lblYourCurrentScore";
            this.lblYourCurrentScore.Size = new System.Drawing.Size(35, 13);
            this.lblYourCurrentScore.TabIndex = 0;
            this.lblYourCurrentScore.Text = "label1";
            // 
            // lblOponent1CurrentScore
            // 
            this.lblOponent1CurrentScore.AutoSize = true;
            this.lblOponent1CurrentScore.Location = new System.Drawing.Point(180, 28);
            this.lblOponent1CurrentScore.Name = "lblOponent1CurrentScore";
            this.lblOponent1CurrentScore.Size = new System.Drawing.Size(35, 13);
            this.lblOponent1CurrentScore.TabIndex = 1;
            this.lblOponent1CurrentScore.Text = "label2";
            // 
            // lblOponent2CurrentScore
            // 
            this.lblOponent2CurrentScore.AutoSize = true;
            this.lblOponent2CurrentScore.Location = new System.Drawing.Point(14, 63);
            this.lblOponent2CurrentScore.Name = "lblOponent2CurrentScore";
            this.lblOponent2CurrentScore.Size = new System.Drawing.Size(35, 13);
            this.lblOponent2CurrentScore.TabIndex = 2;
            this.lblOponent2CurrentScore.Text = "label3";
            // 
            // lblOponent3CurrentScore
            // 
            this.lblOponent3CurrentScore.AutoSize = true;
            this.lblOponent3CurrentScore.Location = new System.Drawing.Point(180, 63);
            this.lblOponent3CurrentScore.Name = "lblOponent3CurrentScore";
            this.lblOponent3CurrentScore.Size = new System.Drawing.Size(35, 13);
            this.lblOponent3CurrentScore.TabIndex = 3;
            this.lblOponent3CurrentScore.Text = "label4";
            // 
            // gbTotalScore
            // 
            this.gbTotalScore.Controls.Add(this.lblYourTotalScore);
            this.gbTotalScore.Controls.Add(this.lblOponent1TotalScore);
            this.gbTotalScore.Controls.Add(this.lblOponent2TotalScore);
            this.gbTotalScore.Controls.Add(this.lblOponent3TotalScore);
            this.gbTotalScore.Location = new System.Drawing.Point(6, 131);
            this.gbTotalScore.Name = "gbTotalScore";
            this.gbTotalScore.Size = new System.Drawing.Size(321, 100);
            this.gbTotalScore.TabIndex = 4;
            this.gbTotalScore.TabStop = false;
            this.gbTotalScore.Text = "Total Score Summary";
            // 
            // lblOponent3TotalScore
            // 
            this.lblOponent3TotalScore.AutoSize = true;
            this.lblOponent3TotalScore.Location = new System.Drawing.Point(180, 63);
            this.lblOponent3TotalScore.Name = "lblOponent3TotalScore";
            this.lblOponent3TotalScore.Size = new System.Drawing.Size(35, 13);
            this.lblOponent3TotalScore.TabIndex = 3;
            this.lblOponent3TotalScore.Text = "label4";
            // 
            // lblOponent2TotalScore
            // 
            this.lblOponent2TotalScore.AutoSize = true;
            this.lblOponent2TotalScore.Location = new System.Drawing.Point(14, 63);
            this.lblOponent2TotalScore.Name = "lblOponent2TotalScore";
            this.lblOponent2TotalScore.Size = new System.Drawing.Size(35, 13);
            this.lblOponent2TotalScore.TabIndex = 2;
            this.lblOponent2TotalScore.Text = "label3";
            // 
            // lblOponent1TotalScore
            // 
            this.lblOponent1TotalScore.AutoSize = true;
            this.lblOponent1TotalScore.Location = new System.Drawing.Point(180, 28);
            this.lblOponent1TotalScore.Name = "lblOponent1TotalScore";
            this.lblOponent1TotalScore.Size = new System.Drawing.Size(35, 13);
            this.lblOponent1TotalScore.TabIndex = 1;
            this.lblOponent1TotalScore.Text = "label2";
            // 
            // lblYourTotalScore
            // 
            this.lblYourTotalScore.AutoSize = true;
            this.lblYourTotalScore.Location = new System.Drawing.Point(14, 28);
            this.lblYourTotalScore.Name = "lblYourTotalScore";
            this.lblYourTotalScore.Size = new System.Drawing.Size(35, 13);
            this.lblYourTotalScore.TabIndex = 0;
            this.lblYourTotalScore.Text = "label1";
            // 
            // GameSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 261);
            this.Controls.Add(this.gbScore);
            this.Controls.Add(this.lblColorCardValue);
            this.Controls.Add(this.lblColorCard);
            this.Controls.Add(this.gbBiddingDetails);
            this.Name = "GameSummary";
            this.Text = "GameSummary";
            this.gbBiddingDetails.ResumeLayout(false);
            this.gbBiddingDetails.PerformLayout();
            this.gbScore.ResumeLayout(false);
            this.gbCurrentScore.ResumeLayout(false);
            this.gbCurrentScore.PerformLayout();
            this.gbTotalScore.ResumeLayout(false);
            this.gbTotalScore.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBiddingDetails;
        private System.Windows.Forms.Label lblColorCard;
        private System.Windows.Forms.Label lblColorCardValue;
        private System.Windows.Forms.Label lblOponent3Bid;
        private System.Windows.Forms.Label lblOponent2bid;
        private System.Windows.Forms.Label lblOponent1Bid;
        private System.Windows.Forms.Label lblYourBid;
        private System.Windows.Forms.GroupBox gbScore;
        private System.Windows.Forms.GroupBox gbCurrentScore;
        private System.Windows.Forms.GroupBox gbTotalScore;
        private System.Windows.Forms.Label lblOponent3TotalScore;
        private System.Windows.Forms.Label lblOponent2TotalScore;
        private System.Windows.Forms.Label lblOponent1TotalScore;
        private System.Windows.Forms.Label lblYourTotalScore;
        private System.Windows.Forms.Label lblOponent3CurrentScore;
        private System.Windows.Forms.Label lblOponent2CurrentScore;
        private System.Windows.Forms.Label lblOponent1CurrentScore;
        private System.Windows.Forms.Label lblYourCurrentScore;
    }
}