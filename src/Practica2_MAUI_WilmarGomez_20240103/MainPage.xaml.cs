namespace Practica2_MAUI_WilmarGomez_20240103;

public partial class MainPage : ContentPage
{
	private bool isAnimating = true;
	private bool isGameRunning = false;
	private int score = 0;
	private int highScore = 0;
	private int timeLeft = 10;
	private IDispatcherTimer? gameTimer;

	public MainPage()
	{
		InitializeComponent();
		StartFloatingAnimation();
		StartEntryAnimations();
	}

	private async void StartEntryAnimations()
	{
		BotContainer.Opacity = 0;
		BotContainer.Scale = 0.5;
		TitleLabel.Opacity = 0;
		GameContainer.Opacity = 0;
		GameContainer.TranslationY = 50;

		await Task.Delay(200);

		await Task.WhenAll(
			BotContainer.FadeToAsync(1, 800, Easing.CubicOut),
			BotContainer.ScaleToAsync(1, 800, Easing.SpringOut)
		);

		await TitleLabel.FadeToAsync(1, 500, Easing.CubicOut);

		await Task.WhenAll(
			GameContainer.FadeToAsync(1, 500, Easing.CubicOut),
			GameContainer.TranslateToAsync(0, 0, 500, Easing.CubicOut)
		);
	}

	private async void StartFloatingAnimation()
	{
		while (isAnimating)
		{
			await BotImage.TranslateToAsync(0, -8, 2000, Easing.SinInOut);
			await BotImage.TranslateToAsync(0, 8, 2000, Easing.SinInOut);
		}
	}

	private async void OnGameButtonClicked(object? sender, EventArgs e)
	{
		await GameBtn.ScaleToAsync(0.95, 50, Easing.CubicIn);
		await GameBtn.ScaleToAsync(1, 100, Easing.SpringOut);

		if (!isGameRunning)
		{
			StartGame();
		}
		else
		{
			score++;
			ScoreLabel.Text = score.ToString();

			await ScoreLabel.ScaleToAsync(1.3, 50, Easing.CubicOut);
			await ScoreLabel.ScaleToAsync(1, 50, Easing.CubicIn);

			if (score % 5 == 0)
			{
				_ = BotImage.RotateToAsync(BotImage.Rotation + 360, 400, Easing.CubicOut);
			}
		}
	}

	private void StartGame()
	{
		isGameRunning = true;
		score = 0;
		timeLeft = 10;

		ScoreLabel.Text = "0";
		TimerLabel.Text = "10s";
		GameBtn.Text = "CLICK!";
		GameBtn.BackgroundColor = Color.FromArgb("#8B7355");
		StatusLabel.Text = "Toca lo mas rapido posible!";

		gameTimer = Dispatcher.CreateTimer();
		gameTimer.Interval = TimeSpan.FromSeconds(1);
		gameTimer.Tick += OnTimerTick;
		gameTimer.Start();
	}

	private async void OnTimerTick(object? sender, EventArgs e)
	{
		timeLeft--;
		TimerLabel.Text = $"{timeLeft}s";

		await TimerLabel.ScaleToAsync(1.2, 100, Easing.CubicOut);
		await TimerLabel.ScaleToAsync(1, 100, Easing.CubicIn);

		if (timeLeft <= 3)
		{
			TimerLabel.TextColor = Color.FromArgb("#C8A2A2");
		}

		if (timeLeft <= 0)
		{
			EndGame();
		}
	}

	private async void EndGame()
	{
		gameTimer?.Stop();
		isGameRunning = false;

		bool isNewRecord = score > highScore;
		if (isNewRecord)
		{
			highScore = score;
			HighScoreLabel.Text = highScore.ToString();
		}

		GameBtn.Text = "JUGAR DE NUEVO";
		GameBtn.BackgroundColor = Color.FromArgb("#2D2926");
		TimerLabel.TextColor = Color.FromArgb("#8B4513");
		TimerLabel.Text = "10s";

		if (isNewRecord)
		{
			StatusLabel.Text = $"Nuevo record: {score} clicks!";
			StatusLabel.TextColor = Color.FromArgb("#7B8B6F");

			await BotImage.RotateToAsync(BotImage.Rotation + 720, 800, Easing.CubicOut);
			await GameContainer.ScaleToAsync(1.03, 200, Easing.CubicOut);
			await GameContainer.ScaleToAsync(1, 200, Easing.CubicIn);
		}
		else if (score >= 20)
		{
			StatusLabel.Text = $"Excelente! {score} clicks";
			StatusLabel.TextColor = Color.FromArgb("#7B8B6F");
		}
		else if (score >= 10)
		{
			StatusLabel.Text = $"Bien! {score} clicks";
			StatusLabel.TextColor = Color.FromArgb("#C8B88A");
		}
		else
		{
			StatusLabel.Text = $"Sigue intentando! {score} clicks";
			StatusLabel.TextColor = Color.FromArgb("#B5AFA5");
		}
	}

	private async void OnDetallesClicked(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new DetallesPage());
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		isAnimating = false;
		gameTimer?.Stop();
	}
}
