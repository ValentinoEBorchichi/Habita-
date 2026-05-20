public class Animal
{
	public string especie;
	public string color;
	public float altura;


	public Animal(string especie, string color, float altura)
	{
		this.especie = especie;
		this.color = color;
		this.altura = altura;
	}

	public void HacerSonido()
	{
		Console.WriteLine("El animal hace un sonido");
	}
	public void Comer()
	{
		Console.WriteLine("El animal está comiendo");
	}
}
